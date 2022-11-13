

namespace Dal;

public class DalOrders
{
    public int Create(DO.Orders o)
    {
        int i = DataSource.Config.amountOrder;
        DataSource.Config.amountOrder++;
        DataSource.orders[i] = o;
        return DataSource.orders[i].ID;
    }

    public DO.Orders Read(int id)
    {
        int i = 0;
        while (DataSource.orders[i].ID != id && i < DataSource.Config.amountOrder)
        {
            i++;
        }
        if (i < DataSource.Config.amountOrder)
            return DataSource.orders[i];
        throw new Exception("The object is not exsist");
    }

 
    public DO.Orders[] ReadAll()
    {
        DO.Orders[] temp = new DO.Orders[DataSource.Config.amountOrder];
        for (int i = 0; i < DataSource.Config.amountOrder; i++)
        {
            temp[i] = DataSource.orders[i];
        }
        return temp;
    }

    public void Delete(int id)
    {
        int i = 0;
        while (DataSource.orders[i].ID != id && i < DataSource.Config.amountOrder)
        {
            i++;
        }
        if (i >= DataSource.Config.amountOrder)
            throw new Exception("The object is not exsist");
        for (int j = i; j < DataSource.Config.amountOrder; j++)
        {
            DataSource.orders[j] = DataSource.orders[j + 1];
        }
        DataSource.Config.amountOrder--;
    }


    public void Update(DO.Orders o)
    {
        int i;
        for (i = 0; i < DataSource.Config.amountOrder; i++)
        {
            if (DataSource.orders[i].ID == o.ID)
            {
                DataSource.orders[i] = o;
                break;
            }
        }
        if (i >= DataSource.Config.amountOrder)
        {
            throw new Exception("The object is not exsist");
        }
    }
}
