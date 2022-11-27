using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using BO;
using Dal;
using DalApi;
using DO;


namespace BlImplementation;

internal class BlProduct : BLApi.IProduct
{
    public IDal Dal = new DalList();
    public IEnumerable<BO.ProductForList> GetAll()
    {
        IEnumerable<BO.ProductForList> productList = null;
        try {
            IEnumerable<DO.Product> products = Dal.Product.GetAll();
            foreach (DO.Product product in products)
            {
                BO.ProductForList productInList = new BO.ProductForList();
                productInList.ID = product.ID;
                productInList.Name = product.Name;
                productInList.Price = product.Price;
                //productList.Category = product.Category; //לא עובד כנראה יש בעיה עם הenum
                productList.ToList().Add(productInList);

            }
        }
        catch (ExceptionFromDal e)
        {
            throw new BO.ExceptionFromDal(e);
        }

        return productList;
    }

    public IEnumerable<ProductItem> GetForCatalog()
    {
        IEnumerable<BO.ProductItem> productList = null;
        try
        {
            IEnumerable<DO.Product> products = Dal.Product.GetAll();
            foreach (DO.Product product in products)
            {
                ProductItem productItem = new ProductItem();
                productItem.ID = product.ID;
                productItem.Name = product.Name;
                productItem.Price = product.Price;
                productItem.Amount = product.Amount;
                //productItem.Category = product.Category;
                if (product.Amount > 0)
                {
                    productItem.InStock = true;
                }
                else
                {
                    productItem.InStock = false;
                }
            }
        }
        catch (ExceptionFromDal e)
        {
            throw new BO.ExceptionFromDal(e);
        }
        return productList;
    }
    public BO.Product GetForManegar(int idProduct) {
        BO.Product bproduct = new BO.Product();
        if (idProduct > 0) { 
            DO.Product dproduct = new DO.Product();
            try {
                dproduct = Dal.Product.Get(idProduct);
                bproduct.ID = dproduct.ID;
                bproduct.Name = dproduct.Name;
                bproduct.Price = dproduct.Price;
                bproduct.Color=dproduct.Color;
                //bproduct.Category = dproduct.Category;//לא עובד כנראה יש בעיה עם הenum
                bproduct.InStock = dproduct.Amount;

            }
            catch (ExceptionFromDal e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
        return bproduct;

    }


    public ProductItem GetForClient(int idProduct, Cart c)
    {
        BO.ProductItem bproductItem = new BO.ProductItem();
        if (idProduct > 0)
        {
            DO.Product dproduct = new DO.Product();
            try
            {
                dproduct = Dal.Product.Get(idProduct);
                bproductItem.ID = dproduct.ID;
                bproductItem.Name = dproduct.Name;
                bproductItem.Price = dproduct.Price;
                bproductItem.Amount = dproduct.Amount;
                //bproductItem.Category = dproduct.Category;//לא עובד כנראה יש בעיה עם הenum
                if (dproduct.Amount>=1)
                    bproductItem.InStock = true;
                else
                    bproductItem.InStock = false;
            }
            catch (ExceptionFromDal e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
        return bproductItem;
    }
    
    
    public void Add(BO.Product p)
    {
        if(p.ID>0 && p.Name !=" " && p.Price>0 && p.InStock>0)
        {
            DO.Product dproduct = new DO.Product();
            dproduct.ID = p.ID;
            dproduct.Name = p.Name;
            dproduct.Price = p.Price;
            dproduct.Amount = p.InStock;
            dproduct.Color = p.Color;
            //dproduct.Category = p.Category;
            try
            {
                int id = Dal.Product.Add(dproduct);
            }
            catch (ExceptionFromDal e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
        else
        {
            throw new exception1();
        }
    }

    public void Delete(int idProduct) {
        
        IEnumerable<DO.OrderItem> temp = Dal.OrderItem.GetAll();
        foreach (DO.OrderItem productInOrder in temp)
        {
            if (productInOrder.ProductID == idProduct)
            {
                throw new exception2();
            }
        }
        try
        {
            Dal.Product.Delete(idProduct);
        }
        catch (ExceptionFromDal e)
        {
            throw new BO.ExceptionFromDal(e);
        }


    }
    
    
    public void Update(BO.Product p)
    {
        if (p.ID > 0 && p.Name != " " && p.Price > 0 && p.InStock > 0)
        {
            DO.Product dproduct = new DO.Product();
            dproduct.ID = p.ID;
            dproduct.Name = p.Name;
            dproduct.Price = p.Price;
            dproduct.Amount = p.InStock;
            dproduct.Color = p.Color;
            //dproduct.Category = p.Category;
            try
            {
                Dal.Product.Update(dproduct);
            }
            catch (ExceptionFromDal e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
    }

}