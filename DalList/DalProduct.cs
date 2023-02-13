using System;
using System.Collections.Generic;
using DalApi;
using DO;
namespace Dal;

internal class DalProduct :IProduct
{
    public bool Exsist(int id)//פונקציית עזר לפונקציית add
    {
        //int i = 0;
        //while (i < DataSource.products.Count)
        //{
        //    if (DataSource.products[i].ID == id)
        //        return true;
        //    i++;
        //}
        //return false;
        bool cheak = false;
        DataSource.products.ForEach(product =>
        {
            if (product.ID == id)
            {
                cheak = true;
            }
        });
       return cheak;    
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
        //int i = 0;
        //while (i < DataSource.products.Count && DataSource.products[i].ID != id)
        //{
        //    i++;
        //}
        //if(i < DataSource.products.Count)
        //    return DataSource.products[i];
        //throw new ex1();


        DO.Product product = (from item in DataSource.products
                    where item.ID == id
                    select item).FirstOrDefault();
       if(product.ID!=0)
            return product;
        throw new ex1();
    }
    public Product Get(Predicate<Product> func)
    {
        return DataSource.products.Find(func);
    }


    public IEnumerable<DO.Product> GetAll(Func<Product, bool>? func = null)
    {
        //List<DO.Product> temp = new List<DO.Product>();
        //for (int i = 0; i < DataSource.products.Count; i++)
        //{
        //    temp.Add(DataSource.products[i]);
        //}
        //return (func == null) ? temp : temp.Where(func);


        var products = (from item in DataSource.products
                        select item).ToList();
        return products;


    }

    public void Delete(int id)
    {
        //int i = 0;
        //while (i < DataSource.products.Count && DataSource.products[i].ID != id)
        //{
        //    i++;
        //}
        //if (i >= DataSource.products.Count)
        //    throw new ex1();
        //else
        //    DataSource.products.RemoveAt(i);
        bool check = false;
        DataSource.products.ForEach(product =>
        {
            if (product.ID == id)
            {
                DataSource.products.Remove(product);
                check = true;
                return;
            }
        });

        if (!check)
        {
            throw new ex1();
        }

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
        if (i >= DataSource.products.Count)
        {
            throw new ex1();
        }
        //bool check = false;  //not update
        //DataSource.products.ForEach(product =>
        //{
        //    if (product.ID == p.ID)
        //    {
        //        product.ID = p.ID;
        //        product.Name = p.Name;
        //        product.Price=p.Price;
        //        product.Category=p.Category;
        //        product.Color=p.Color;
        //        product.Amount = p.Amount;
        //        check=true;
        //    }
        //});
        //if (!check)
        //{
        //    throw new ex1();
        //}

    }
}

