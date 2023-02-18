using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DalApi;
using System.Net.Mail;
using BO;
using System.Reflection;
using DO;
using System.Diagnostics;

namespace BlImplementation;

internal class BlCart : ICart
{
    IDal? dal = DalApi.Factory.Get();
    public BO.Cart AddProductToCart(BO.Cart c, int productId)
    {

        var returnProduct = c.Items?.Where(item => (item.ProductID == productId)).Select((item, i) => new { product = dal?.Product.Get(item.ProductID), index = i }).FirstOrDefault();
        if (returnProduct != null)
        {
            DO.Product? product = returnProduct.product;
            int index = returnProduct.index;
            if (product?.Amount > c.Items?[index].Amount)
            {
                c.Items[index].Amount++;
                c.Items[index].TotalPrice += c.Items[index].Price;
                c.TotalPrice += c.Items[index].Price;
            }
            else
            {
                throw new BO.notEnoughAmount(); //חריגה אין מספיק במלאי
            }
        }
        else
        {
            try
            {
                DO.Product product = dal?.Product.Get(productId) ?? throw new nullException();
                if (product.Amount > 0)
                {
                    BO.OrderItem orderItem = new BO.OrderItem();
                    orderItem.ID = c.Items?.Count+1 ?? throw new nullException();
                    orderItem.Amount = 1;
                    orderItem.Price = product.Price;
                    orderItem.TotalPrice = product.Price;
                    orderItem.Name = product.Name;
                    orderItem.ProductID = productId;
                    c.Items?.Add(orderItem);
                    c.TotalPrice += orderItem.Price;
                }
            }
            catch (ex1 e)
            {
                throw new BO.ExceptionFromDal(e);//לא נמצא האוביקט
            }
        }
        return c;

        //foreach (var item in c.Items)
        //{
        //    if (item.ProductID == productId)
        //    {
        //        DO.Product product = dal.Product.Get(item.ProductID);
        //        if (product.Amount > item.Amount)
        //        {
        //            item.Amount++;
        //            item.TotalPrice += item.Price;
        //            c.TotalPrice += item.Price;
        //            return c;
        //        }
        //        else
        //        {
        //            throw new BO.notEnoughAmount(); //חריגה אין מספיק במלאי
        //        }
        //    }
        //}
        //try
        //{
        //    DO.Product product = dal.Product.Get(productId);
        //    if (product.Amount > 0)
        //    {
        //        BO.OrderItem orderItem = new BO.OrderItem();
        //        orderItem.ID = c.Items.Count + 1;
        //        orderItem.Amount = 1;
        //        orderItem.Price = product.Price;
        //        orderItem.TotalPrice = product.Price;
        //        orderItem.Name = product.Name;
        //        orderItem.ProductID = productId;
        //        c.Items.Add(orderItem);
        //        c.TotalPrice += orderItem.Price;
        //    }
        //}
        //catch (ex1 e)
        //{
        //    throw new BO.ExceptionFromDal(e);//לא נמצא האוביקט
        //}
        //return c;//האם מחזירים בכל מצב?
    }
    

    public BO.Cart Update(BO.Cart c, int productId, int amount)
    {
        BO.OrderItem orderItem=c.Items?.Find(item => item.ProductID == productId)?? throw new nullException();
        if (orderItem != null)
        {
            if (orderItem.Amount < amount)
            {
                DO.Product product;
                try
                {
                    product = dal?.Product.Get(orderItem.ProductID) ?? throw new nullException();
                }
                catch (Exception)
                {
                    throw;
                }
                if (product.Amount > amount)
                {
                    c.TotalPrice -= orderItem.Price * orderItem.Amount;
                    orderItem.Amount = amount;
                    orderItem.TotalPrice = orderItem.Price * amount;
                    c.TotalPrice += orderItem.Price * orderItem.Amount;
                }
                else
                {
                    throw new BO.notEnoughAmount();
                }
            }
            else if (amount < orderItem.Amount && amount != 0)
            {
                c.TotalPrice -= orderItem.TotalPrice;
                orderItem.TotalPrice = orderItem.Price * amount;
                orderItem.Amount = amount;
                c.TotalPrice += orderItem.TotalPrice;
            }
            else
            {
                c.TotalPrice -= orderItem.TotalPrice;
                c.Items.Remove(orderItem);
            }
        }

        return c;
        //foreach (var item in c.Items)
        //{
        //    if (item.ProductID == productId)
        //    {
        //        if (amount > item.Amount)
        //        {
        //            DO.Product product;
        //            try
        //            {
        //                product=dal?.Product.Get(item.ProductID) ?? throw new nullException();
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //            if (product.Amount > amount)
        //            {
        //                c.TotalPrice -= item.Price * item.Amount;
        //                item.Amount = amount;
        //                item.TotalPrice = item.Price * amount;
        //                c.TotalPrice += item.Price * item.Amount;
        //                break;
        //            }
        //            else
        //            {
        //                throw new BO.notEnoughAmount();
        //            }
        //        }
        //        else if (amount < item.Amount && amount != 0)
        //        {
        //            c.TotalPrice -= item.TotalPrice;
        //            item.TotalPrice = item.Price * amount;
        //            item.Amount = amount;
        //            c.TotalPrice += item.TotalPrice;
        //            break;
        //        }
        //        else
        //        {
        //            c.TotalPrice -= item.TotalPrice;
        //            c.Items.Remove(item);
        //            break;
        //        }
        //    }
        //}
        //return c;
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
        DO.Orders order = new DO.Orders 
        { CustomerName = name, CustomerEmail = email, CustomerAdress = address, DeliveryDate = DateTime.MinValue, ShipDate = DateTime.MinValue, OrderDate = DateTime.Now };
        int id;
        id = dal?.Order.Add(order) ?? throw new nullException();
        int idItemInOrder;
        c.Items?.ForEach(item =>
        {
            DO.OrderItem itemInOrder = new DO.OrderItem { OrderID = id, ProductID = item.ProductID, Price = item.Price, Amount = item.Amount };
            idItemInOrder = dal.OrderItem.Add(itemInOrder);
            DO.Product product = dal.Product.Get(item.ProductID);
            product.Amount -= item.Amount;
            try
            {
                dal.Product.Update(product);
            }
            catch (ex1 e)
            {
                throw new BO.ExceptionFromDal(e);//the object is exsist
            }
        });
    }
}