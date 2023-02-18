using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;
namespace Dal;

internal class DalOrders:IOrder
{
    public int Add(DO.Orders o)
    {
        o.ID = DataSource.Config.Order;
        DataSource.orders.Add(o);
        return o.ID;
    }

    public Orders Get(int id)
    {
        //int i = 0;
        //while (i < DataSource.orders.Count && DataSource.orders[i].ID != id)
        //{
        //    i++;
        //}
        //if (i < DataSource.orders.Count)
        //    return DataSource.orders[i];
        //throw new ex1();

        DO.Orders order1 = (from order in DataSource.orders
                              where order.ID == id
                              select order).FirstOrDefault();
        if (order1.ID != 0)
            return order1;
        throw new ex1();
    }
    public Orders Get(Predicate<Orders> func)
    {
        return DataSource.orders.Find(func);
    }

    public IEnumerable<DO.Orders> GetAll(Func<Orders, bool>? func = null)
    {
        //List<Orders> temp = new List<Orders>();
        //for (int i = 0; i < DataSource.orders.Count; i++)
        //{
        //    temp.Add(DataSource.orders[i]);
        //}
        //return (func == null) ? temp : temp.Where(func);

        var orders = (from item in DataSource.orders
                        orderby item.OrderDate
                        select item).ToList();
        return (func == null) ? orders : orders.Where(func);//
    }

    public void Delete(int id)
    {
        //int i = 0;
        //while (DataSource.orders[i].ID != id && i < DataSource.orders.Count)
        //{
        //     DataSource.orders.RemoveAt(i);
        //}
        //if (i >= DataSource.orders.Count)
        //    throw new ex1();

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
