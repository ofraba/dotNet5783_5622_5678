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
        //List<BO.OrderForList> ordersList = new List<BO.OrderForList>();
        //IEnumerable<DO.Orders> orders = dal.Order.GetAll();
        //IEnumerable<DO.OrderItem> itemInOrderList = dal.OrderItem.GetAll();
        //foreach (DO.Orders order in orders)
        //{
        //    BO.OrderForList orderForList = new BO.OrderForList();
        //    orderForList.ID = order.ID;
        //    orderForList.CustomerName = order.CustomerName;
        //    int amountMone = 0;
        //    double price = 0;
        //    foreach (DO.OrderItem itemInOrder in itemInOrderList)
        //    {
        //        if (itemInOrder.ID == order.ID)
        //        {
        //            amountMone += itemInOrder.Amount;
        //            price += itemInOrder.Price;
        //        }
        //    }
        //    orderForList.TotalPrice = price;
        //    orderForList.AmountOfItems = amountMone;
        //    orderForList.status = Status(order);
        //    ordersList.Add(orderForList);

        //}
        //return (func == null) ? ordersList : ordersList.Where(func);
        
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


        //BO.Order order2 = new BO.Order();
        //try
        //{
        //    DO.Orders order = dal.Order.Get(idOrder);
        //    BO.OrderStatus status = Status(order);
        //    if (status == BO.OrderStatus.sent)
        //    {
        //        DateTime today = DateTime.Now;
        //        DO.Orders order1 = new DO.Orders { ID = order.ID, CustomerName = order.CustomerName, CustomerEmail = order.CustomerEmail, CustomerAdress = order.CustomerAdress, OrderDate = order.OrderDate, ShipDate = order.ShipDate, DeliveryDate = today };
        //        dal.Order.Update(order1);
        //        IEnumerable<DO.OrderItem> itemInOrderList = dal.OrderItem.GetAll();
        //        order2.ID = order.ID;
        //        order2.CustomerName = order.CustomerName;
        //        order2.CustomerEmail = order.CustomerEmail;
        //        order2.CustomerAddress = order.CustomerAdress;
        //        order2.Status = BO.OrderStatus.provided;
        //        order2.OrderDate = order.OrderDate;
        //        order2.ShipDate = order.ShipDate;
        //        order2.DeliveryDate = today;
        //        double totalPrice = 0;
        //        foreach (var itemInOrder in itemInOrderList)
        //        {
        //            if (itemInOrder.ID == idOrder)
        //            {
        //                BO.OrderItem orderItem = new BO.OrderItem();
        //                orderItem.ID = itemInOrder.ID;
        //                orderItem.Amount = itemInOrder.Amount;
        //                orderItem.Price = itemInOrder.Price;
        //                orderItem.ProductID = itemInOrder.ProductID;
        //                orderItem.TotalPrice = itemInOrder.Price * itemInOrder.Amount;
        //                if (order2.Items == null)
        //                {
        //                    order2.Items = new List<BO.OrderItem>();
        //                }
        //                order2.Items.Add(orderItem);
        //                totalPrice += itemInOrder.Price * itemInOrder.Amount;
        //            }
        //        }
        //        order2.TotalPrice = totalPrice;
        //    }

        //    else
        //    {
        //        throw new BO.orderHasBeenDelivered();//זריקת חריגה ההזמנה סופקה
        //    }
        //}
        //catch (ex1 e)
        //{
        //    throw new BO.ExceptionFromDal(e);
        //}
        //return order2;
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
    
}
