
using DalApi;
using DO;
using System.Collections.Generic;
namespace Dal;


internal class DalOrderItem :IOrderItem
{
    public int Add(DO.OrderItem orderItem)
    {
        DataSource.orderItems.Add(orderItem);
        return orderItem.ID;
    }
    public DO.OrderItem Get(int id)
    {
        int i = 0;
        while (DataSource.orderItems[i].ID != id && i < DataSource.orderItems.Count)
        {
            i++;
        }
        if (i < DataSource.orderItems.Count)
            return DataSource.orderItems[i];
        throw new ex1();
    }

    public IEnumerable<DO.OrderItem> GetAll()
    {
        List<DO.OrderItem> temp = new List<DO.OrderItem>();
        for (int i = 0; i < DataSource.orderItems.Count; i++)
        {
            temp[i] = DataSource.orderItems[i];
        }
        return temp;
    }

    public void Delete(int id)
    {
        int i = 0;
        while (DataSource.orderItems[i].ID != id && i < DataSource.orderItems.Count)
        {
            DataSource.orderItems.RemoveAt(i);
        }
        if (i >= DataSource.orderItems.Count)
            throw new ex1();
    }
    public void Update(DO.OrderItem ot)
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

    public OrderItem FindOrderItem(int idProduct, int idOrder)
    {
        int i;
        for (i = 0; i < DataSource.orderItems.Count; i++)
        {
            if (DataSource.orderItems[i].OrderID == idOrder && DataSource.orderItems[i].ProductID == idProduct)
                break;
        }
        if (i == DataSource.orderItems.Count)
            throw new ex1();
        else
            return DataSource.orderItems[i];
    }

    public IEnumerable<OrderItem> FindAllOrderItem(int idOrder)
    {
        IEnumerable<OrderItem> list = null;
        for (int i = 0; i < DataSource.orderItems.Count; i++)
        {
            if (DataSource.orderItems[i].OrderID == idOrder)
            {
                list.ToList().Add(DataSource.orderItems[i]);
            }
        }
        return list;
    }
}
