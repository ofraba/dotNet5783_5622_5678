using System.Collections.Generic;
namespace Dal;

public class DalOrderItem
{
    public int Create(DO.OrderItem orderItem)
    {
        DataSource.orderItems.Add(orderItem);
        return orderItem.ID;
    }
    public DO.OrderItem Read(int id)
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

    public List<DO.OrderItem> ReadAll()
    {
        List<DO.OrderItem> temp = new List<DO.OrderItem>();
        DO.OrderItem[] temp = new DO.OrderItem[DataSource.orderItems.Count];
        for (int i = 0; i < DataSource.orderItems.Count; i++)
        {
            temp[i] = DataSource.orderItems[i];
        }
        return temp;
    }

    public void Delete(int id)
    {
        int i = 0;
        while (DataSource.orderItems[i].ID != id && i < DataSource.Config.amountOrderItem)
        {
            DataSource.orderItems.RemoveAt(i);
        }
        if (i >= DataSource.Config.amountOrderItem)
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
}
