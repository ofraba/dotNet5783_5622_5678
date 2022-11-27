using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using BO;
using Dal;
using DalApi;


namespace BlImplementation;

internal class BlOrder : BLApi.IOrder
{
    public IDal dal = new DalList();



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



    public IEnumerable<BO.OrderForList> GetAll()
    {
        IEnumerable<BO.OrderForList> ordersList = null;
        IEnumerable<DO.Orders> orders = dal.Order.GetAll();
        IEnumerable<DO.OrderItem> itemInOrderList = dal.OrderItem.GetAll();
        foreach (DO.Orders order in orders)
        {
            BO.OrderForList orderForList = new BO.OrderForList();
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustomerName;
            int amountMone = 0;
            double price = 0;
            foreach (DO.OrderItem itemInOrder in itemInOrderList)
            {
                if (itemInOrder.ID == order.ID)
                {
                    amountMone += itemInOrder.Amount;
                    price += itemInOrder.Price;
                }
            }
            orderForList.TotalPrice = price;
            orderForList.AmountOfItems = amountMone;
            orderForList.status = Status(order);
            ordersList.ToList().Add(orderForList);

        }
        return ordersList;
    }

    public BO.Order GetForManegar(int idOrder)
    {
        BO.Order bOrder = new BO.Order();
        if (idOrder > 0)
        {
            DO.Orders dOrder = new DO.Orders();
            try
            {
                dOrder = dal.Order.Get(idOrder);
                bOrder.ID = dOrder.ID;
                bOrder.CustomerName = dOrder.CustomerName;
                bOrder.CustomerEmail = dOrder.CustomerEmail;
                bOrder.CustomerAddress = dOrder.CustomerAdress;
                bOrder.OrderDate = dOrder.OrderDate;
                bOrder.ShipDate = dOrder.ShipDate;
                bOrder.DeliveryDate = dOrder.DeliveryDate;
                IEnumerable<DO.OrderItem> itemInOrderList = dal.OrderItem.GetAll();
                double totalPrice = 0;
                foreach (var itemInOrder in itemInOrderList)
                {
                    if (itemInOrder.ID == idOrder)
                    {
                        BO.OrderItem orderItem = new BO.OrderItem();
                        orderItem.ID = itemInOrder.ID;
                        orderItem.Amount = itemInOrder.Amount;
                        orderItem.Price = itemInOrder.Price;
                        orderItem.ProductID = itemInOrder.ProductID;
                        orderItem.TotalPrice = itemInOrder.Price * itemInOrder.Amount;
                        bOrder.Items.ToList().Add(orderItem);
                        totalPrice += itemInOrder.Price * itemInOrder.Amount;

                    }
                }
                bOrder.TotalPrice = totalPrice;
            }
            catch (ExceptionFromDal e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
        else
        {
            throw new exception3();
        }
        return bOrder;

    }
    //עדכון שילוח הזמנה
    public BO.Order OrderShippingUpdate(int idOrder)
    {
        BO.Order order2 = new BO.Order();
        try
        {
            DO.Orders order = dal.Order.Get(idOrder);
            BO.OrderStatus status = Status(order);
            if (status == BO.OrderStatus.confirmed)
            {
                DateTime today = DateTime.Now;
                DO.Orders order1 = new DO.Orders { ID = order.ID, CustomerName = order.CustomerName, CustomerEmail = order.CustomerEmail, CustomerAdress = order.CustomerAdress, OrderDate = order.OrderDate, ShipDate = today, DeliveryDate = order.DeliveryDate };
                dal.Order.Update(order1);
                IEnumerable<DO.OrderItem> itemInOrderList = dal.OrderItem.GetAll();
                order2.ID = order.ID;
                order2.CustomerName = order.CustomerName;
                order2.CustomerEmail = order.CustomerEmail;
                order2.CustomerAddress = order.CustomerAdress;
                order2.Status = BO.OrderStatus.sent;
                order2.OrderDate = order.OrderDate;
                order2.ShipDate = today;
                order2.DeliveryDate = order.DeliveryDate;

                double totalPrice = 0;
                foreach (var itemInOrder in itemInOrderList)
                {
                    if (itemInOrder.ID == idOrder)
                    {
                        BO.OrderItem orderItem = new BO.OrderItem();
                        orderItem.ID = itemInOrder.ID;
                        orderItem.Amount = itemInOrder.Amount;
                        orderItem.Price = itemInOrder.Price;
                        orderItem.ProductID = itemInOrder.ProductID;
                        orderItem.TotalPrice = itemInOrder.Price * itemInOrder.Amount;
                        order2.Items.ToList().Add(orderItem);
                        totalPrice += itemInOrder.Price * itemInOrder.Amount;
                    }
                }
                order2.TotalPrice = totalPrice;

            }

            else
            {
                throw new exception4();
            }
        }
        catch (ExceptionFromDal e)
        {
            throw new BO.ExceptionFromDal(e);
        }
        return order2;
    }



    //עדכון אספקת הזמנה
    public BO.Order OrderDeliveryUpdate(int idOrder)
    {
        BO.Order order2 = new BO.Order();
        try
        {
            DO.Orders order = dal.Order.Get(idOrder);
            BO.OrderStatus status = Status(order);
            if (status == BO.OrderStatus.sent)
            {
                DateTime today = DateTime.Now;
                DO.Orders order1 = new DO.Orders { ID = order.ID, CustomerName = order.CustomerName, CustomerEmail = order.CustomerEmail, CustomerAdress = order.CustomerAdress, OrderDate = order.OrderDate, ShipDate = order.ShipDate, DeliveryDate = today };
                dal.Order.Update(order1);
                IEnumerable<DO.OrderItem> itemInOrderList = dal.OrderItem.GetAll();
                order2.ID = order.ID;
                order2.CustomerName = order.CustomerName;
                order2.CustomerEmail = order.CustomerEmail;
                order2.CustomerAddress = order.CustomerAdress;
                order2.Status = BO.OrderStatus.provided;
                order2.OrderDate = order.OrderDate;
                order2.ShipDate = order.ShipDate;
                order2.DeliveryDate = today;
                double totalPrice = 0;
                foreach (var itemInOrder in itemInOrderList)
                {
                    if (itemInOrder.ID == idOrder)
                    {
                        BO.OrderItem orderItem = new BO.OrderItem();
                        orderItem.ID = itemInOrder.ID;
                        orderItem.Amount = itemInOrder.Amount;
                        orderItem.Price = itemInOrder.Price;
                        orderItem.ProductID = itemInOrder.ProductID;
                        orderItem.TotalPrice = itemInOrder.Price * itemInOrder.Amount;
                        order2.Items.ToList().Add(orderItem);
                        totalPrice += itemInOrder.Price * itemInOrder.Amount;
                    }
                }
                order2.TotalPrice = totalPrice;
            }

            else
            {
                throw new exception5();//זריקת חריגה ההזמנה סופקה
            }
        }
        catch (ExceptionFromDal e)
        {
            throw new BO.ExceptionFromDal(e);
        }
        return order2;
    }

    //מעקב הזמנה
    //public void OrderTracking(int idOrder)
    //{
    //    try
    //    {
    //        DO.Orders order = dal.Order.Get(idOrder);

    //    }
    //    catch
    //    {

    //    }
    //}
}
