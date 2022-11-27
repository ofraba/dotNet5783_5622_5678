using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DalApi;
using Dal;
using System.Net.Mail;

namespace BlImplementation;

internal class BlCart : BLApi.ICart
{
    public IDal dal = new DalList();
    public BO.Cart AddProductToCart(BO.Cart c, int productId)
    {

        foreach (var item in c.Items)
        {
            if (item.ProductID == productId)
            {
                DO.Product product = dal.Product.Get(item.ProductID);
                if (product.Amount >= item.Amount)
                {
                    item.Amount++;
                    item.TotalPrice += item.Price;
                    c.TotalPrice += item.Price;
                    return c;
                }
                else
                {
                    throw new BO.exception6(); //חריגה אין מספיק במלאי
                }
            }
        }
        try
        {
            DO.Product product = dal.Product.Get(productId);
            if (product.Amount > 0)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.Amount = 1;
                orderItem.Price = product.Price;
                orderItem.TotalPrice = product.Price;
                orderItem.Name = product.Name;
                orderItem.ProductID = productId;
                c.Items.ToList().Add(orderItem);
                c.TotalPrice += orderItem.Price;
            }
        }
        catch (BO.ExceptionFromDal e)
        {
            throw new BO.ExceptionFromDal(e);//לא נמצא האוביקט
        }
        return c;//האם מחזירים בכל מצב?
    }
    public BO.Cart Update(BO.Cart c, int productId, int amount)
    {
        foreach (var item in c.Items)
        {
            if (item.ProductID == productId)
            {
                if (amount > item.Amount)
                {

                    return AddProductToCart(c, productId);
                }
                else if (amount < item.Amount)
                {
                    c.TotalPrice -= item.TotalPrice;
                    item.TotalPrice = item.Price * amount;
                    item.Amount = amount;
                    c.TotalPrice += item.TotalPrice;
                    break;
                }
                else
                {
                    c.TotalPrice -= item.TotalPrice;
                    c.Items.ToList().Remove(item);
                    break;
                }
            }
        }
        return c;

    }
    public void Confirm(BO.Cart c, string name, string email, string address)
    {
        if (c.CustomerAdress == "" || c.CustomerName == "" || c.CustomerEmail == "")//check if the params are valid
            throw new BO.exception1();
        try
        {
            var mailAddress = new MailAddress(c.CustomerEmail);
        }
        catch
        {
            throw new BO.exception1();
        }
        DO.Orders order = new DO.Orders { ID=0, CustomerName = name , CustomerEmail = email, CustomerAdress = address, DeliveryDate= DateTime.MinValue, ShipDate = DateTime.MinValue, OrderDate = DateTime.Now };
        try
        {
            int id = dal.Order.Add(order);
            foreach (var item in c.Items)
            {
                DO.OrderItem itemInOrder = new DO.OrderItem { ID=0, OrderID = id , ProductID = item.ProductID , Price = item.Price , Amount= item.Amount };
                dal.OrderItem.Add(itemInOrder);
                //catch (BO.ExceptionFromDal e)
                //{
                //    throw new BO.ExceptionFromDal(e);//מספר הזמנה כבר קיים
                //}
                DO.Product product= dal.Product.Get(item.ProductID);
                product.Amount -= item.Amount;
                dal.Product.Update(product);
            }
        }
        catch (BO.ExceptionFromDal e)
        {
            throw new BO.ExceptionFromDal(e);//אוביקט כבר קיים
        }


    }
}
