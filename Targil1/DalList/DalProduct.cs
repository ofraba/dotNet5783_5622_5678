

namespace Dal;

public class DalProduct
{

    public bool Exsist(int id)
    {
        int i = 0;
        while (i < DataSource.Config.amountProducts)
        {
            if (DataSource.products[i].ID == id)
                return true;
            i++;
        }
        return false;
    }
    public int Create(DO.Product p)
    {
        int id = p.ID;
        bool isExsist = Exsist(id);
        if (isExsist)
        { 
            Console.WriteLine("The object is exsist");
            throw new Exception("The object is exsist");
    }
        int index = DataSource.Config.amountProducts;
        DataSource.products[index] = p;
        DataSource.Config.amountProducts++;
        return id;
    }

    public DO.Product Read(int id)
    {
        int i = 0;
        while (DataSource.products[i].ID != id && i < DataSource.Config.amountProducts)
        {
            i++;
        }
        if(i < DataSource.Config.amountProducts)
            return DataSource.products[i];
        throw new Exception("The object is not exsist");
    }


    public DO.Product[] ReadAll()
    {
        DO.Product[] temp = new DO.Product[DataSource.Config.amountProducts];
        for (int i = 0; i < DataSource.Config.amountProducts; i++)
        {
            temp[i] = DataSource.products[i];
        }
        return temp;
    }
    public void Delete(int id)
    {
        int i = 0;
        while (DataSource.products[i].ID != id && i < DataSource.Config.amountProducts)
        {
            i++;
        }
        if (i >= DataSource.Config.amountProducts)
            throw new Exception("The object is not exsist");
        for (int j = i; j < DataSource.Config.amountProducts; j++)
        {
            DataSource.products[j] = DataSource.products[j + 1];
        }
        DataSource.Config.amountProducts--;
    }


    public void Update(DO.Product p)
    {
        int i;
        for (i = 0; i < DataSource.Config.amountProducts; i++)
        {
            if (DataSource.products[i].ID == p.ID)
            {
                DataSource.products[i] = p;
                break;
            }
        }
        if (i>= DataSource.Config.amountProducts)
        {
            throw new Exception("The object is not exsist");
        }
    }
}

