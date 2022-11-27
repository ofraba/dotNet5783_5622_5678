using Dal;
using DO;
using DalApi;
using System;
using System.Collections.Generic;


namespace DalTest
{
   

    static class Program
    {
        static private IDal dalList = new DalList();
        Random rnd = new Random();
       
        static void Main(string[] args)
        {
            Console.WriteLine("enter 0 for exit,\n 1 for product, \n 2 for orders, \n 3 for items in order:");
            int number = int.Parse(Console.ReadLine());
            while (number != 0)
            {
                switch (number)
                {
                    case 1:
                        Console.WriteLine("enter a for add a prduct,\n b for show a prduct,\n c for show all of the products,\n d for update, \n e for delete:");
                        char pChoose = Convert.ToChar(Console.ReadLine());
                        productFunction(pChoose);
                        break;
                    case 2:
                        Console.WriteLine("enter a for add a order,\n b for show a order,\n c for show all of the orders,\n d for update, \n e for delete:");
                        char oChoose = Convert.ToChar(Console.ReadLine());
                        ordersFunction(oChoose);
                        break;
                    case 3:
                        Console.WriteLine("enter a for add item in order,\n b for show item in order,\n c for show all of the items in order,\n d for update, \n e for delete:");
                        char iChoose = Convert.ToChar(Console.ReadLine());
                        itemInOrderFunction(iChoose);
                        break;
                    default:
                        break;
                }
            }

            static void productFunction(char p)
            {
                double productPrice;
                int productId, productAmount;
                string productName, productCategory, productColor;
                switch (p)
                {
                    case 'a':
                        productId = Convert.ToInt32(rnd.Next(100000, 999999));//cheak if it work
                        Console.WriteLine("enter name of the product");
                        productName = Console.ReadLine();
                        Console.WriteLine("enter price of the product");
                        productPrice = double.Parse(Console.ReadLine());
                        Console.WriteLine("enter color of the product");
                        productColor = Console.ReadLine();
                        Console.WriteLine("enter category\n 1 for dinnerware\n 2 for linen\n 3 for bathAccessories\n 4 for styling\n 5 for textile");
                        int category = int.Parse(Console.ReadLine());
                        string chooseCategory;
                        switch (category)
                        {
                            case 1:
                                chooseCategory = Convert.ToString(DO.Category.dinnerware);
                                break;
                            case 2:
                                chooseCategory = Convert.ToString(DO.Category.linen);
                                break;
                            case 3:
                                chooseCategory = Convert.ToString(DO.Category.bathAccessories);
                                break;
                            case 4:
                                chooseCategory = Convert.ToString(DO.Category.styling);
                                break;
                            case 5:
                                chooseCategory = Convert.ToString(DO.Category.textile);
                                break;
                            default:
                                break;
                        }
                        Console.WriteLine("enter amount of the product");
                        productAmount = int.Parse(Console.ReadLine());
                        Product newProduct = new Product
                        {
                            ID = productId,
                            Name = productName,
                            Price = productPrice,
                            Color = productColor,
                            Category = chooseCategory,//איך ושיםקטגוריה?
                            Amount = productAmount
                        };
                        try
                        {
                            int id = dalList.Product.Add(newProduct);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'b':
                        Console.WriteLine("enter id of prduct you want to see");
                        int idToSearch = int.Parse(Console.ReadLine());
                        Product product = new Product();
                        try
                        {
                            product = dalList.Product.Get(idToSearch);
                            Console.WriteLine("id: " + product.ID);
                            Console.WriteLine("name: " + product.Name);
                            Console.WriteLine("price: " + product.Price);
                            Console.WriteLine("color: " + product.Color);
                            Console.WriteLine("category: " + product.Category);
                            Console.WriteLine("amount: " + product.Amount);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'c':
                        IEnumerable<Product> getAll = dalList.Product.GetAll();
                        foreach (var item in getAll)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 'd':
                        Console.WriteLine("enter id of prduct you want to update");
                        int idToUpdte = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter name to update:");
                        productName = Console.ReadLine();
                        Console.WriteLine("enter price to update:");
                        productPrice = double.Parse(Console.ReadLine());
                        Console.WriteLine("enter color to update:");
                        productColor = Console.ReadLine();
                        Console.WriteLine("enter Category to update:");
                        productCategory = Console.ReadLine();
                        Console.WriteLine("enter amount to update:");
                        productAmount = int.Parse(Console.ReadLine());
                        Product updateProdut = new Product
                        {
                            ID = idToUpdte,
                            Name = productName,
                            Price = productPrice,
                            Color = productColor,
                            Category = productCategory,
                            Amount = productAmount
                        };
                        try
                        {
                            dalList.Product.Update(updateProdut);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'e':
                        Console.WriteLine("enter id of product to delete");
                        int idToDelete= int.Parse(Console.ReadLine());
                        try {
                            dalList.Product.Delete(idToDelete);
                        }
                        catch (Exception exception) {
                            Console.WriteLine(exception);
                        }
                        break;
                    default: break;
                }

            }
            static void ordersFunction(char o)
            {
                DateTime orderDate, shipDate, deliveryDate;
                int orderId;
                string customerName, customerEmail, customerAdress;
                switch (o)
                {
                    case 'a':
                        orderId = DataSource.Config.Order;
                        Console.WriteLine("enter customer's name:");
                        customerName = Console.ReadLine();
                        Console.WriteLine("enter  customer's email:");
                        customerEmail = Console.ReadLine();
                        Console.WriteLine("enter customer's address");
                        customerAdress = Console.ReadLine();
                        Console.WriteLine("enter date of order");
                        orderDate = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("enter date of ship");
                        shipDate = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("enter date of delivery");
                        deliveryDate = Convert.ToDateTime(Console.ReadLine());
                        Orders newOrder = new Orders
                        {
                            ID = orderId,
                            CustomerName = customerName,
                            CustomerEmail = customerEmail,
                            CustomerAdress = customerAdress,
                            OrderDate = orderDate,
                            ShipDate = shipDate,//האם אמורים להכניס את זה
                            DeliveryDate= deliveryDate//האם אמורים להכניס את זה
                        };
                        try
                        {
                            dalList.Order.Add(newOrder);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'b':
                        Console.WriteLine("enter id of order you want to see");
                        int idToSearch = int.Parse(Console.ReadLine());
                        Orders order = new Orders();
                        try
                        {
                            order = dalList.Order.Get(idToSearch);
                            Console.WriteLine("id: " + order.ID);
                            Console.WriteLine("customer's name:: " + order.CustomerName);
                            Console.WriteLine("customer's email: " + order.CustomerEmail);
                            Console.WriteLine("customer's address:" + order.CustomerAdress);
                            Console.WriteLine("date of order: " + order.OrderDate);
                            Console.WriteLine("date of ship: " + order.ShipDate);
                            Console.WriteLine("date of delivery: " + order.DeliveryDate);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'c':
                        IEnumerable<Orders> allOrders = dalList.Order.GetAll();
                        foreach (var item in allOrders)
                        {
                            Console.WriteLine(item);
                        }
                        //for (int i = 0; i < allOrders.Length; i++)
                        //{
                        //    Console.WriteLine(allOrders[i]);
                        //}
                        break;
                    case 'd':
                        Console.WriteLine("enter id of order you want to update");
                        int idToUpdte = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter customer's name to update:");
                        customerName = Console.ReadLine();
                        Console.WriteLine("enter customer's email to update:");
                        customerEmail = Console.ReadLine();
                        Console.WriteLine("enter customer's address to update:");
                        customerAdress = Console.ReadLine();
                        Console.WriteLine("enter date of order to update:");
                        orderDate = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("enter date of ship to update:");
                        shipDate = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("enter date of delivery to update:");
                        deliveryDate = Convert.ToDateTime(Console.ReadLine());
                        Orders updateOrder = new Orders
                        {
                            ID = idToUpdte,
                            CustomerName = customerName,
                            CustomerEmail = customerEmail,
                            CustomerAdress = customerAdress,
                            OrderDate = orderDate,
                            ShipDate = shipDate,
                            DeliveryDate = deliveryDate
                        };
                        try
                        {
                            dalList.Order.Update(updateOrder);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'e':
                        Console.WriteLine("enter id of product to delete");
                        int idToDelete = int.Parse(Console.ReadLine());
                        try
                        {
                            dalList.Order.Delete(idToDelete);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    default: break;
                }
            }
            static void itemInOrderFunction(char i)
            {
                double price;
                int productId,orderId,id,amount;
                switch (i)
                {
                    case 'a':
                        id= DataSource.Config.ItemInOrder;
                        Console.WriteLine("enter id of the order");
                        orderId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter id of the product");
                        productId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter price");
                        price = double.Parse(Console.ReadLine());
                        Console.WriteLine("enter amount");
                        amount = int.Parse(Console.ReadLine());
                        OrderItem newOrderItem = new OrderItem
                        {
                            ID = id,
                            ProductID = productId,
                            OrderID = orderId,
                            Price = price,
                            Amount = amount
                        };
                        try
                        {
                            dalList.OrderItem.Add(newOrderItem);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'b':
                        Console.WriteLine("enter id of items in order");
                        int idToSearch = int.Parse(Console.ReadLine());
                        OrderItem orderItem = new OrderItem();
                        try
                        {
                            orderItem = dalList.OrderItem.Get(idToSearch);
                            Console.WriteLine("id: " + orderItem.ID);
                            Console.WriteLine("id of product: " + orderItem.ProductID);
                            Console.WriteLine("id of order: " + orderItem.OrderID);
                            Console.WriteLine("price: " + orderItem.Price);
                            Console.WriteLine("amount: " + orderItem.Amount);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'c':
                        IEnumerable<OrderItem> allOrderItem = dalList.OrderItem.GetAll();
                        foreach (var item in allOrderItem)
                        {
                            Console.WriteLine(item);
                        }
                        //for (int j = 0; j < AllOrderItem.Length; j++)
                        //{
                        //    Console.WriteLine(AllOrderItem[j]);
                        //}
                        break;
                    case 'd':
                        Console.WriteLine("enter id of the items in order you want to update");
                        int idToUpdte = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter id of product to update:");
                        productId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter id of order to update:");
                        orderId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter price to update:");
                        price = double.Parse(Console.ReadLine());
                        Console.WriteLine("enter amount to update:");
                        amount = int.Parse(Console.ReadLine());
                        OrderItem updateOrderItem = new OrderItem
                        {
                            ID = idToUpdte,
                            ProductID = productId,
                            OrderID = orderId,
                            Price = price,
                            Amount = amount
                        };
                        try
                        {
                            dalList.OrderItem.Update(updateOrderItem);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'e':
                        Console.WriteLine("enter id of items in order to delete");
                        int idToDelete = int.Parse(Console.ReadLine());
                        try
                        {
                            dalList.OrderItem.Delete(idToDelete);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    default: break;
                }


            }
        }

    }
}