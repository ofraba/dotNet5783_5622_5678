using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using Dal;
using DalApi;



namespace BlImplementation;

internal class BlProduct : BLApi.IProduct
{
    public IDal Dal = new DalList();
    public IEnumerable<BO.ProductForList> GetAll(Func<BO.ProductForList, bool>? func = null)
    {
        List<BO.ProductForList> productList = new List<BO.ProductForList>();

        IEnumerable<DO.Product> products = Dal.Product.GetAll();
        foreach (DO.Product product in products)
        {
            BO.ProductForList productInList = new BO.ProductForList();
            productInList.ID = product.ID;
            productInList.Name = product.Name;
            productInList.Price = product.Price;
            productInList.Category = (BO.Category)product.Category;
            productList.Add(productInList);
        }
        return (func == null) ? productList : productList.Where(func);
    }

    public IEnumerable<BO.ProductItem> GetForCatalog()
    {
        List<BO.ProductItem> productList = new List<BO.ProductItem>();

        IEnumerable<DO.Product> products = Dal.Product.GetAll();
        foreach (DO.Product product in products)
        {
            BO.ProductItem productItem = new BO.ProductItem();
            productItem.ID = product.ID;
            productItem.Name = product.Name;
            productItem.Price = product.Price;
            productItem.Amount = product.Amount;
            productItem.Category = (BO.Category)product.Category;
            productItem.Color=product.Color;
            if (product.Amount > 0)
            {
                productItem.InStock = true;
            }
            else
            {
                productItem.InStock = false;
            }
            productList.Add(productItem);
        }
        return productList;
    }


    public BO.Product GetForManegar(int idProduct)
    {
        BO.Product bproduct = new BO.Product();
        if (idProduct > 0)
        {
            DO.Product dproduct = new DO.Product();
            try
            {
                dproduct = Dal.Product.Get(idProduct);
                bproduct.ID = dproduct.ID;
                bproduct.Name = dproduct.Name;
                bproduct.Price = dproduct.Price;
                bproduct.Color = dproduct.Color;
                bproduct.Category = (BO.Category)dproduct.Category;//לא עובד כנראה יש בעיה עם הenum
                bproduct.InStock = dproduct.Amount;

            }
            catch (ex1 e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
        return bproduct;

    }


    public BO.ProductItem GetForClient(int idProduct, BO.Cart c)
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
                bproductItem.Category = (BO.Category)dproduct.Category;
                bproductItem.Color=dproduct.Color;
                if (dproduct.Amount >= 1)
                    bproductItem.InStock = true;
                else
                    bproductItem.InStock = false;
            }
            catch (BO.ExceptionFromDal e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
        return bproductItem;
    }


    public int Add(BO.Product p)
    {
        if (p.ID > 0 && p.Name != " " && p.Price > 0 && p.InStock > 0)
        {
            DO.Product dproduct = new DO.Product();
            dproduct.ID = p.ID;
            dproduct.Name = p.Name;
            dproduct.Price = p.Price;
            dproduct.Amount = p.InStock;
            dproduct.Color = p.Color;
            dproduct.Category = (DO.Category)p.Category;
            try
            {
                return Dal.Product.Add(dproduct);
            }
            catch (ex2 e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
        else
        {
            throw new BO.dataIsntInvalid();
        }
    }

    public void Delete(int idProduct)
    {

        IEnumerable<DO.OrderItem> temp = Dal.OrderItem.GetAll();
        foreach (DO.OrderItem productInOrder in temp)
        {
            if (productInOrder.ProductID == idProduct)
            {
                throw new BO.productExsistInOrder();
            }
        }
        try
        {
            Dal.Product.Delete(idProduct);
        }
        catch (ex1 e)
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
            dproduct.Category = (DO.Category)p.Category;
            try
            {
                Dal.Product.Update(dproduct);
            }
            catch (ex1 e)
            {
                throw new BO.ExceptionFromDal(e);
            }
        }
    }

}