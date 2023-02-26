
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dal;


internal class DalOrderItem : IOrderItem
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.ItemInOrder;
        DataSource.orderItems.Add(orderItem);
        return orderItem.ID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem Get(int id)
    {
        //int i = 0;
        //while (DataSource.orderItems[i].ID != id && i < DataSource.orderItems.Count)
        //{
        //    i++;
        //}
        //if (i < DataSource.orderItems.Count)
        //    return DataSource.orderItems[i];
        //throw new ex1();
        DO.OrderItem orderItem1 = (from orderItem in DataSource.orderItems
                                   where orderItem.ID == id
                                   select orderItem).FirstOrDefault();
        if (orderItem1.ID != 0)
            return orderItem1;
        throw new ex1();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem Get(Predicate<OrderItem> func)
    {
        return DataSource.orderItems.Find(func);
    }
    public IEnumerable<DO.OrderItem> GetAll(Func<OrderItem, bool>? func = null)
    {
        var orderItems = (from item in DataSource.orderItems
                          select item).ToList();
        return (func == null) ? orderItems : orderItems.Where(func);//
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        bool check = false;
        DataSource.orderItems.ForEach(orderItem =>
        {
            if (orderItem.ID == id)
            {
                DataSource.orderItems.Remove(orderItem);
                check = true;
                return;
            }
        });

        if (!check)
        {
            throw new ex1();
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem ot)
    {
        int i;
        for (i = 0; i < DataSource.orderItems.Count; i++)
        {
            if (DataSource.orderItems[i].ID == ot.ID)
            {
                DataSource.orderItems[i] = ot;
                break;
            }
        }
        if (i >= DataSource.orderItems.Count)
        {
            throw new ex1();
        }
    }

    //public OrderItem FindOrderItem(int idProduct, int idOrder)
    //{
    //    int i;
    //    for (i = 0; i < DataSource.orderItems.Count; i++)
    //    {
    //        if (DataSource.orderItems[i].OrderID == idOrder && DataSource.orderItems[i].ProductID == idProduct)
    //            break;
    //    }
    //    if (i == DataSource.orderItems.Count)
    //        throw new ex1();
    //    else
    //        return DataSource.orderItems[i];
    //}


    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> FindAllOrderItem(int idOrder)
    {
        List<OrderItem> list = new List<OrderItem>();
        for (int i = 0; i < DataSource.orderItems.Count; i++)
        {
            if (DataSource.orderItems[i].OrderID == idOrder)
            {
                list.Add(DataSource.orderItems[i]);
            }
        }
        return list;
    }
}
