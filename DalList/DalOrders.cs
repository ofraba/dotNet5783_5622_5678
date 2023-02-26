using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DalApi;
using DO;
namespace Dal;

internal class DalOrders:IOrder
{

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Orders o)
    {
        o.ID = DataSource.Config.Order;
        DataSource.orders.Add(o);
        return o.ID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Orders Get(int id)
    {
        DO.Orders order1 = (from order in DataSource.orders
                              where order.ID == id
                              select order).FirstOrDefault();
        if (order1.ID != 0)
            return order1;
        throw new ex1();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Orders Get(Predicate<Orders> func)
    {
        return DataSource.orders.Find(func);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Orders> GetAll(Func<Orders, bool>? func = null)
    {
        var orders = (from item in DataSource.orders
                        orderby item.OrderDate
                        select item).ToList();
        return (func == null) ? orders : orders.Where(func);//
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        bool check = false;
        DataSource.orders.ForEach(order =>
        {
            if (order.ID == id)
            {
                DataSource.orders.Remove(order);
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
    public void Update(DO.Orders o)
    {
        //int i;
        //for (i = 0; i < DataSource.orders.Count; i++)
        //{
        //    if (DataSource.orders[i].ID == o.ID)
        //    {
        //        DataSource.orders[i] = o;
        //        break;
        //    }
        //}
        //if (i >= DataSource.orders.Count)
        //{
        //    throw new ex1();
        //}
        var orderToUpdate = DataSource.orders.Where(order=> order.ID == o.ID).Select((item, i) => new { index = i }).FirstOrDefault();
        if (orderToUpdate == null)
        {
            throw new ex1();
        }
        DataSource.orders[orderToUpdate.index] = o;
    }
}
