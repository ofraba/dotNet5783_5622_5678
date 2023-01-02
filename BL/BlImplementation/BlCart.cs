using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DalApi;
using System.Net.Mail;

namespace BlImplementation;

internal class BlCart : ICart
{
    IDal? dal = DalApi.Factory.Get();
    public BO.Cart AddProductToCart(BO.Cart c, int productId)
    {
        //var x = from item in c.Items
        //        where item.ProductID == productId
        //        let product = dal.Product.Get(item.ProductID)
        //        where product.Amount > item.Amount
        //        select new { amount = item.Amount++, totalPrice= item.TotalPrice += item.Price, cTotalPrice = c.TotalPrice += item.Price};
        foreach (var item in c.Items)
        {
            if (item.ProductID == productId)
            {
                DO.Product product = dal.Product.Get(item.ProductID);
                if (product.Amount > item.Amount)
                {
                    item.Amount++;
                    item.TotalPrice += item.Price;
                    c.TotalPrice += item.Price;
                    return c;
                }
                else
                {
                    throw new BO.notEnoughAmount(); //חריגה אין מספיק במלאי
                }
            }
        }
        try
        {
            DO.Product product = dal.Product.Get(productId);
            if (product.Amount > 0)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = c.Items.Count + 1;
                orderItem.Amount = 1;
                orderItem.Price = product.Price;
                orderItem.TotalPrice = product.Price;
                orderItem.Name = product.Name;
                orderItem.ProductID = productId;
                c.Items.Add(orderItem);
                c.TotalPrice += orderItem.Price;
            }
        }
        catch (ex1 e)
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

                    DO.Product product = dal.Product.Get(item.ProductID);
                    if (product.Amount > amount)
                    {
                        c.TotalPrice -= item.Price * item.Amount;
                        item.Amount = amount;
                        item.TotalPrice = item.Price * amount;
                        c.TotalPrice += item.Price * item.Amount;
                        break;
                    }
                    else
                    {
                        throw new BO.notEnoughAmount();
                    }
                }
                else if (amount < item.Amount && amount != 0)
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
                    c.Items.Remove(item);
                    break;
                }
            }
        }

        return c;

    }
    public void Confirm(BO.Cart c, string name, string email, string address)
    {
        if (address == "" || name == "" || email == "")//check if the data are valid
            throw new BO.dataIsntInvalid();
        try
        {
            var mailAddress = new MailAddress(email);
        }
        catch
        {
            throw new BO.dataIsntInvalid();
        }
        DO.Orders order = new DO.Orders { ID = 0, CustomerName = name, CustomerEmail = email, CustomerAdress = address, DeliveryDate = DateTime.MinValue, ShipDate = DateTime.MinValue, OrderDate = DateTime.Now };

        int id = dal.Order.Add(order);
        foreach (var item in c.Items)
        {
            DO.OrderItem itemInOrder = new DO.OrderItem { ID = 0, OrderID = id, ProductID = item.ProductID, Price = item.Price, Amount = item.Amount };
            dal.OrderItem.Add(itemInOrder);
            DO.Product product = dal.Product.Get(item.ProductID);
            product.Amount -= item.Amount;
            try
            {
                dal.Product.Update(product);
            }
            catch (ex1 e)
            {
                throw new BO.ExceptionFromDal(e);//אוביקט כבר קיים
            }
        }
    }
}