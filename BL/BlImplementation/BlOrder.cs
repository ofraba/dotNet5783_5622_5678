using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DalApi;


namespace BlImplementation;

internal class BlOrder : BLApi.IOrder
{
    IDal? dal = DalApi.Factory.Get();


    //The function calculates the status
    private BO.OrderStatus Status(DO.Orders order)
    {
        DateTime today = DateTime.Now;
        if (order.DeliveryDate.CompareTo(today) < 0 && order.DeliveryDate.CompareTo(DateTime.MinValue) != 0)
            return BO.OrderStatus.provided;
        if (order.ShipDate.CompareTo(today) < 0 && order.ShipDate.CompareTo(DateTime.MinValue) != 0)
            return BO.OrderStatus.sent;
        return BO.OrderStatus.confirmed;
    }



    public IEnumerable<BO.OrderForList> GetAll(Func<BO.OrderForList, bool>? func = null)
    {
         var orders=(from order in dal?.Order.GetAll()
                let orderItems = dal?.OrderItem.FindAllOrderItem(order.ID)
                select new BO.OrderForList()
                {
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    AmountOfItems = orderItems.Count(),
                    TotalPrice = orderItems.Sum(order => order.Price),
                    status = Status(order)
                });
        return (func == null) ? orders : orders.Where(func);

    }

    public BO.Order GetForManegar(int idOrder)
    {
        BO.Order bOrder = new BO.Order();
        if (idOrder > 0)
        {
            DO.Orders dOrder = new DO.Orders();
            try
            {
                dOrder = dal?.Order.Get(idOrder) ?? throw new BO.nullException();
                bOrder.ID = dOrder.ID;
                bOrder.CustomerName = dOrder.CustomerName;
                bOrder.CustomerEmail = dOrder.CustomerEmail;
                bOrder.CustomerAddress = dOrder.CustomerAdress;
                bOrder.OrderDate = dOrder.OrderDate;
                bOrder.ShipDate = dOrder.ShipDate;
                bOrder.DeliveryDate = dOrder.DeliveryDate;
                bOrder.Items = new List<BO.OrderItem>();
                double totalPrice = 0;
                var itemInOrderList1 = from itemInOrder in dal.OrderItem.GetAll()
                                        where itemInOrder.OrderID == idOrder
                                        select (new BO.OrderItem()
                                        {
                                           ID = itemInOrder.ID,
                                           Amount = itemInOrder.Amount,
                                           Price = itemInOrder.Price,
                                           ProductID = itemInOrder.ProductID,
                                           TotalPrice = itemInOrder.Price * itemInOrder.Amount,
                                        });
                    itemInOrderList1.ToList().ForEach(item =>
                    {
                        bOrder.Items.Add(item);
                        totalPrice += item.Price * item.Amount;
                    });
                bOrder.TotalPrice = totalPrice;
            }
            catch (ex1 e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
        else
        {
            throw new BO.negativeIDnumber();
        }
        return bOrder;
    }

    //עדכון שילוח הזמנה
    public BO.Order OrderShippingUpdate(int idOrder)
    {
        try
        {
            BO.Order updatedOrder = GetForManegar(idOrder);
            DO.Orders order = dal?.Order.Get(idOrder) ?? throw new BO.nullException(); ;
            if (order.ShipDate == DateTime.MinValue || order.ShipDate.CompareTo(DateTime.Now) > 0)
            {
                order.ShipDate = DateTime.Now;
                dal.Order.Update(order);
                updatedOrder = GetForManegar(idOrder);
                updatedOrder.Status=BO.OrderStatus.sent;
                return updatedOrder;
            }
            else
            {
                throw new BO.TheOrderHasBeenSent();//זריקת חריגה ההזמנה סופקה
            }

        }
        catch (ex1 e)
        {
            throw new BO.ExceptionFromDal(e);
        }
    }



    //עדכון אספקת הזמנה
    public BO.Order OrderDeliveryUpdate(int idOrder)
    {

        try
        {
            BO.Order updatedOrder = GetForManegar(idOrder);
            DO.Orders order = dal?.Order.Get(idOrder) ?? throw new BO.nullException(); ;
            if (order.DeliveryDate == DateTime.MinValue || order.DeliveryDate.CompareTo(DateTime.Now) > 0)
            {
                order.DeliveryDate = DateTime.Now;
                dal.Order.Update(order);
                updatedOrder = GetForManegar(idOrder);
                return updatedOrder;
            }
            else
            {
                throw new BO.orderHasBeenDelivered();//זריקת חריגה ההזמנה סופקה
            }
        }
        catch (ex1 e)
        {
            throw new BO.ExceptionFromDal(e);
        }
    }

    
    public BO.OrderTracking OrderTracking(int idOrder)
    {
        try
        {
            DO.Orders order = dal?.Order.Get(idOrder) ?? throw new BO.nullException(); ;
            BO.OrderStatus status = Status(order);
            List<(DateTime, string)> descriptionAndDate = new List<(DateTime, string)> { };
            if (status == BO.OrderStatus.provided)
            {
                descriptionAndDate.Add((order.DeliveryDate,"the order provied"));
            }
            if(status == BO.OrderStatus.sent)
            {
                descriptionAndDate.Add((order.ShipDate, "the order sent"));
            }
            if(status == BO.OrderStatus.confirmed)
            {
                descriptionAndDate.Add((order.OrderDate, "the order confirmed"));
            }
            BO.OrderTracking orderTracking = new BO.OrderTracking
            {
                ID = idOrder,
                status = status,
                dateAndDescription = descriptionAndDate
            };
            return orderTracking;
        }
        catch (ex1 e)
        {
            throw new BO.ExceptionFromDal(e);
        }
    }

    public int? ChooseOrder()
    {
        IEnumerable<DO.Orders> orderList = dal?.Order.GetAll() ?? throw new BO.nullException();
        DateTime theMinDate= DateTime.Now;
        int numberOfOrder = 0;
        int temp = 0;
        bool cheak = false;
        orderList.ToList().ForEach(item =>
        {
            if (item.DeliveryDate == DateTime.MinValue)
            { 
               if(item.ShipDate<theMinDate && item.OrderDate<theMinDate)
                {
                    numberOfOrder=temp;

                    if(item.ShipDate!=DateTime.MinValue)
                    {
                        theMinDate=item.ShipDate;
                    }
                    else
                    {
                        theMinDate = item.OrderDate;
                    }
                }
            }
            temp++;
        });
        return cheak? numberOfOrder+1:null;
    }

}
