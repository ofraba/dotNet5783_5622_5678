﻿
using System.Collections.Generic;
namespace Dal;

public class DalOrders
{
    public int Create(DO.Orders o)
    {
        DataSource.orders.Add=o;
        return o.ID;
    }

    public DO.Orders Read(int id)
    {
        int i = 0;
        while (DataSource.orders[i].ID != id && i < DataSource.orders.Count)
        {
            i++;
        }
        if (i < DataSource.orders.Count)
            return DataSource.orders[i];
        throw new ex1();
    }

 
    public List<DO.Orders> ReadAll()
    {
        List<DO.Orders> temp = new List<DO.Orders>();
        for (int i = 0; i < DataSource.orders.Count; i++)
        {
            temp[i] = DataSource.orders[i];
        }
        return temp;
    }

    public void Delete(int id)
    {
        int i = 0;
        while (DataSource.orders[i].ID != id && i < DataSource.orders.Count)
        {
             DataSource.orders.RemoveAt(i);
        }
        if (i >= DataSource.Config.amountOrder)
            throw new ex1();
    }


    public void Update(DO.Orders o)
    {
        int i;
        for (i = 0; i < DataSource.orders.Count; i++)
        {
            if (DataSource.orders[i].ID == o.ID)
            {
                DataSource.orders[i] = o;
                break;
            }
        }
        if (i >= DataSource.orders.Count)
        {
            throw new ex1();
        }
    }
}
