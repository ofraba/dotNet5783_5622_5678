using System.Security.Cryptography;
using Dal;
using System;
using DO;
using DalApi;
namespace Dal;

// צריך לממש
sealed public class DalList : IDal
{
    public IProduct Product => new DalProduct();

   

    public IOrder Order => new DalOrders();

  

    public IOrderItem OrderItem => new DalOrderItem();

}
