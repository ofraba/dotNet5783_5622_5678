using Dal;
using DO;
using DalApi;
using System;
using System.Collections.Generic;


namespace DalTest
{
    public class Program
    {
        static private IDal dalList = new DalList();
        static readonly Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("enter 0 for exit,\n 1 for product, \n 2 for orders, \n 3 for items in order:");
            int number;
            int.TryParse(Console.ReadLine(), out number);
            while (number != 0)
            {
                switch (number)
                {
                    case 1:
                        Console.WriteLine("enter a for add a prduct,\n b for show a prduct,\n c for show all of the products,\n d for update, \n e for delete:");
                        char pChoose;
                        char.TryParse(Console.ReadLine(), out pChoose);
                        productFunction(pChoose);
                        break;
                    case 2:
                        Console.WriteLine("enter a for add a order,\n b for show a order,\n c for show all of the orders,\n d for update, \n e for delete:");
                        char oChoose;
                        char.TryParse(Console.ReadLine(), out oChoose);
                        ordersFunction(oChoose);
                        break;
                    case 3:
                        Console.WriteLine("enter a for add item in order,\n b for show item in order,\n c for show all of the items in order," + "\n d for update, \n e for delete,\n f for show all of the products in sfecific order, \n g for show specific product in item by id of orde and id of prduct");
                        char iChoose;
                        char.TryParse(Console.ReadLine(), out iChoose);
                        itemInOrderFunction(iChoose);
                        break;
                    default:
                        break;
                }
            }

            static void productFunction(char p)
            {
                double productPrice;
                int productId, productAmount, productCategory, category;
                string? productName, productColor;
                switch (p)
                {
                    case 'a':
                        productId = Convert.ToInt32(random.Next(100000, 999999));
                        Console.WriteLine("enter name of the product");
                        productName = Console.ReadLine();
                        Console.WriteLine("enter price of the product");
                        double.TryParse(Console.ReadLine(), out productPrice);
                        Console.WriteLine("enter color of the product");
                        productColor = Console.ReadLine();
                        Console.WriteLine("enter category\n 1 for dinnerware\n 2 for linen\n 3 for bathAccessories\n 4 for styling\n 5 for textile");
                        int.TryParse(Console.ReadLine(), out category);
                        Console.WriteLine("enter amount of the product");
                        int.TryParse(Console.ReadLine(), out productAmount);
                        Product newProduct = new Product()
                        {
                            ID = productId,
                            Name = productName,
                            Price = productPrice,
                            Color = productColor,
                            Category = (DO.Category)category,
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
                        int idToSearch;
                        int.TryParse(Console.ReadLine(), out idToSearch);
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
                        Console.WriteLine("enter id of prםduct you want to update");
                        int idToUpdte;
                        int.TryParse(Console.ReadLine(), out idToUpdte);
                        Console.WriteLine("enter name to update:");
                        productName = Console.ReadLine();
                        Console.WriteLine("enter price to update:");
                        double.TryParse(Console.ReadLine(), out productPrice);
                        Console.WriteLine("enter color to update:");
                        productColor = Console.ReadLine();
                        Console.WriteLine("enter Category to update:");
                        int.TryParse(Console.ReadLine(), out productCategory);
                        Console.WriteLine("enter amount to update:");
                        int.TryParse(Console.ReadLine(), out productAmount);
                        Product updateProdut = new Product
                        {
                            ID = idToUpdte,
                            Name = productName,
                            Price = productPrice,
                            Color = productColor,
                            Category = (DO.Category)productCategory,
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
                        int idToDelete;
                        int.TryParse(Console.ReadLine(), out idToDelete);
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
                string? customerName, customerEmail, customerAdress;
                switch (o)
                {
                    case 'a':
                        Orders newOrder = new Orders() { };
                        Console.WriteLine("enter customer's name:");
                        newOrder.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter  customer's email:");
                        newOrder.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter customer's address");
                        newOrder.CustomerAdress = Console.ReadLine();
                        Console.WriteLine("enter date of order");
                        newOrder.OrderDate = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("enter date of ship");
                        newOrder.ShipDate = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("enter date of delivery");
                        newOrder.DeliveryDate = Convert.ToDateTime(Console.ReadLine());
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
                        int idToSearch;
                        int.TryParse(Console.ReadLine(), out idToSearch);
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
                        break;
                    case 'd':
                        Console.WriteLine("enter id of order you want to update");
                        int idToUpdte;
                        int.TryParse(Console.ReadLine(), out idToUpdte);
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
                        int idToDelete;
                        int.TryParse(Console.ReadLine(), out idToDelete);
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
                int productId, orderId, id, amount, idToDelete;
                switch (i)
                {
                    case 'a':
                        OrderItem newOrderItem = new OrderItem() { };
                        Console.WriteLine("enter id of the order");
                        int.TryParse(Console.ReadLine(), out orderId);
                        newOrderItem.OrderID = orderId;
                        Console.WriteLine("enter id of the product");
                        int.TryParse(Console.ReadLine(), out productId);
                        newOrderItem.ProductID = productId;
                        Console.WriteLine("enter price");
                        double.TryParse(Console.ReadLine(), out price);
                        newOrderItem.Price = price;
                        Console.WriteLine("enter amount");
                        int.TryParse(Console.ReadLine(), out amount);
                        newOrderItem.Amount = amount;
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
                        int idToSearch;
                        int.TryParse(Console.ReadLine(), out idToSearch);
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
                        break;
                    case 'd':
                        Console.WriteLine("enter id of the items in order you want to update");
                        int idToUpdte;
                        int.TryParse(Console.ReadLine(), out idToUpdte);
                        Console.WriteLine("enter id of product to update:");
                        int.TryParse(Console.ReadLine(), out productId);
                        Console.WriteLine("enter id of order to update:");
                        int.TryParse(Console.ReadLine(), out orderId);
                        Console.WriteLine("enter price to update:");
                        double.TryParse(Console.ReadLine(), out price);
                        Console.WriteLine("enter amount to update:");
                         int.TryParse(Console.ReadLine(), out amount);
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
                        int.TryParse(Console.ReadLine(), out idToDelete);
                        try
                        {
                            dalList.OrderItem.Delete(idToDelete);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'f':
                        Console.WriteLine("enter id of order you want to see it's products");
                        int.TryParse(Console.ReadLine(), out id);
                        foreach (var item in dalList.OrderItem.GetAll(element => element.OrderID == id))
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 'g':
                        Console.WriteLine("enter id of product and order to see the product in order");
                        int.TryParse(Console.ReadLine(), out id);
                        int.TryParse(Console.ReadLine(), out productId);
                        Console.WriteLine(dalList.OrderItem.Get(element => element.OrderID == id && element.ProductID == productId));
                        break;
                    default: break;
                }


            }
        }

    }
}