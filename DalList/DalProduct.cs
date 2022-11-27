﻿using System.Collections.Generic;
using DalApi;
using DO;
namespace Dal;

internal class DalProduct :IProduct
{

    public bool Exsist(int id)
    {
        int i = 0;
        while (i < DataSource.products.Count)
        {
            if (DataSource.products[i].ID == id)
                return true;
            i++;
        }
        return false;
    }
    public int Add(DO.Product p)
    {
        int id = p.ID;
        bool isExsist = Exsist(id);
        if (isExsist)
        { 
            throw new ex2();
    }
        DataSource.products.Add(p);
        return id;
    }

    public DO.Product Get(int id)
    {
        int i = 0;
        while (DataSource.products[i].ID != id && i < DataSource.products.Count)
        {
            i++;
        }
        if(i < DataSource.products.Count)
            return DataSource.products[i];
        throw new ex1();
    }



    public IEnumerable<DO.Product> GetAll()
    {
        List<DO.Product> temp = new List<DO.Product>();
        for (int i = 0; i < DataSource.products.Count; i++)
        {
            temp[i] = DataSource.products[i];
        }
        return temp;
    }

    public void Delete(int id)
    {
        int i = 0;
        while (DataSource.products[i].ID != id && i < DataSource.products.Count)
        {
            DataSource.products.RemoveAt(i);
        }
        if (i >= DataSource.products.Count)
            throw new ex1();
    }


    public void Update(DO.Product p)
    {
        int i;
        for (i = 0; i < DataSource.products.Count; i++)
        {
            if (DataSource.products[i].ID == p.ID)
            {
                DataSource.products[i] = p;
                break;
            }
        }
        if (i>= DataSource.products.Count)
        {
            throw new ex1();
        }
    }

   
}

