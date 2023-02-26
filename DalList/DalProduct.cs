using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DalApi;
using DO;
namespace Dal;

internal class DalProduct : IProduct
{

    public bool Exsist(int id)//פונקציית עזר לפונקציית add
    {
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

    [MethodImpl(MethodImplOptions.Synchronized)]
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

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Product Get(int id)
    {
        DO.Product product = (from item in DataSource.products
                              where item.ID == id
                              select item).FirstOrDefault();
        if (product.ID != 0)
            return product;
        throw new ex1();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(Predicate<Product> func)
    {
        return DataSource.products.Find(func);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Product> GetAll(Func<Product, bool>? func = null)
    {
        var products = (from item in DataSource.products
                        orderby item.Category
                        select item).ToList();
        return (func == null) ? products : products.Where(func);//
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        bool check = false;
        DataSource.products.ForEach(product =>
        {
            if (product.ID == id)
            {
                DataSource.products.Remove(product);
                check = true;
            }
        });

        if (check == false)
        {
            throw new ex1();
        }

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
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

            //            var productToUpdate = DataSource.products.Where(product => product.ID == p.ID).Select((item, i) => new { index = i }).FirstOrDefault();
            //    if (productToUpdate == null)
            //    {
            //        throw new ex1();
            //    }
            //    DataSource.products[productToUpdate.index] = p;
        }
    }
}

