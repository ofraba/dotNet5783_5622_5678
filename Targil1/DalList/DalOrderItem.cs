
namespace Dal;

public class DalOrderItem
{
    public int Create(DO.OrderItem orderItem)
    {
        int i= DataSource.Config.amountOrderItem;
        DataSource.Config.amountOrderItem++;
        DataSource.orderItems[i]=orderItem;
        return DataSource.orderItems[i].ID;
    }
    public DO.OrderItem Read(int id)
    {
        int i = 0;
        while (DataSource.orderItems[i].ID != id && i < DataSource.Config.amountOrderItem)
        {
            i++;
        }
        if (i < DataSource.Config.amountOrderItem)
            return DataSource.orderItems[i];
        throw new Exception("The object is not exsist");
    }

    public DO.OrderItem[] ReadAll()
    {
        DO.OrderItem[] temp = new DO.OrderItem[DataSource.Config.amountOrderItem];
        for (int i = 0; i < DataSource.Config.amountOrderItem; i++)
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
            i++;
        }
        if (i >= DataSource.Config.amountOrderItem)
            throw new Exception("The object is not exsist");
        for (int j = i; j < DataSource.Config.amountOrderItem; j++)
        {
            DataSource.orderItems[j] = DataSource.orderItems[j + 1];
        }
        DataSource.Config.amountOrderItem--;
    }
    public void Update(DO.OrderItem ot)
    {
        int i;
        for (i = 0; i < DataSource.Config.amountOrderItem; i++)
        {
            if (DataSource.orderItems[i].ID == ot.ID)
            {
                DataSource.orderItems[i] = ot;
                break;
            }
        }
        if (i >= DataSource.Config.amountOrderItem)
        {
            throw new Exception("The object is not exsist");
        }
    }
}
