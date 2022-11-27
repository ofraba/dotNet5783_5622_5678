using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class BlOrder:BLApi.IOrder
{
    public IDal dal = new DalList();
    public IEnumerable<BO.OrderForList> GetAll()
    {
        IEnumerable<BO.OrderForList> ordersList = null;
        IEnumerable<DO.Orders> orders = dal.Order.GetAll();
        IEnumerable<DO.OrderItem> itemInOrderList = dal.OrderItem.GetAll();
        foreach (DO.Orders order in orders)
        {
            BO.OrderForList orderForList= new BO.OrderForList();
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustomerName;
            int amountMone = 0;
            double price = 0;
            foreach (DO.OrderItem itemInOrder in itemInOrderList) { 
                if(itemInOrder.ID == order.ID)
                {
                    amountMone += itemInOrder.Amount;
                    price += itemInOrder.Price;
                }
            }
            orderForList.TotalPrice= price;
            orderForList.AmountOfItems= amountMone;
            //orderForList.status=//לבדוק איך יודעים מה מצב ההזמנה
            ordersList.ToList().Add(orderForList);

        }
        return ordersList;
    }


    public BO.Order GetForManegar(int idOrder)
    {
        BO.Order bOrder= new BO.Order();
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
                        totalPrice+= itemInOrder.Price * itemInOrder.Amount;

                    }
                }
             bOrder.TotalPrice = totalPrice;
            }
            catch
            {
                //לזרוק חריגה
            }
        }
        return bOrder;

    }
    //עדכון שילוח הזמנה
    public Product OrderShippingUpdate(int idOrder);
    //עדכון אספקת הזמנה
    public int OrderDeliveryUpdate(int idOrder);

    //מעקב הזמנה
    public void OrderTracking(int idOrder);

}
