using Dal;
using DO;
using System;



namespace DalTest
{

    class Program
    {
        private static DalProduct dProduct = new DalProduct();
        private static DalOrders dOrders = new DalOrders();
        private static DalOrderItem dItemInOrder = new DalOrderItem();

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
                        Console.WriteLine("enter id of the product");
                        productId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter name of the product");
                        productName = Console.ReadLine();
                        Console.WriteLine("enter price of the product");
                        productPrice = double.Parse(Console.ReadLine());
                        Console.WriteLine("enter color of the product");
                        productColor = Console.ReadLine();
                        Console.WriteLine("enter category of the product");
                        productCategory = Console.ReadLine();
                        Console.WriteLine("enter amount of the product");
                        productAmount = int.Parse(Console.ReadLine());
                        Product newProduct = new Product
                        {
                            ID = productId,
                            Name = productName,
                            Price = productPrice,
                            color = productColor,
                            Category = productCategory,
                            amount = productAmount
                        };
                        try
                        {
                            int id=dProduct.Create(newProduct);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);//toString
                        }
                        break;
                    case 'b':
                        Console.WriteLine("enter id of prduct you want to see");
                        int idToSearch = int.Parse(Console.ReadLine());
                        Product product = new Product();
                        try
                        {
                            product = dProduct.Read(idToSearch);
                            Console.WriteLine("id: " + product.ID);
                            Console.WriteLine("name: " + product.Name);
                            Console.WriteLine("price: " + product.Price);
                            Console.WriteLine("color: " + product.color);
                            Console.WriteLine("category: " + product.Category);
                            Console.WriteLine("amount: " + product.amount);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        break;
                    case 'c':
                        Product[] readAll =dProduct.ReadAll();
                        foreach (var item in readAll)
                        {
                            Console.WriteLine(item);
                        }
                        //for (int i = 0; i < readAll.Length; i++)
                        //{
                        //    Console.WriteLine(readAll[i]);
                        //}
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
                            color = productColor,
                            Category = productCategory,
                            amount = productAmount
                        };
                        try
                        {
                            dProduct.Update(updateProdut);
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
                            dProduct.Delete(idToDelete);
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
                        Console.WriteLine("enter id of the order");
                        orderId = int.Parse(Console.ReadLine());
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
                            ShipDate = shipDate,
                            DeliveryDate= deliveryDate
                        };
                        try
                        {
                            dOrders.Create(newOrder);
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
                            order = dOrders.Read(idToSearch);
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
                        Orders[] allOrders = dOrders.ReadAll();
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
                            dOrders.Update(updateOrder);
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
                            dOrders.Delete(idToDelete);
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
                        Console.WriteLine("enter id of the items in order");
                        id = int.Parse(Console.ReadLine());
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
                            dItemInOrder.Create(newOrderItem);
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
                            orderItem = dItemInOrder.Read(idToSearch);
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
                        OrderItem[] AllOrderItem = dItemInOrder.ReadAll();
                        foreach (var item in AllOrderItem)
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
                            dItemInOrder.Update(updateOrderItem);
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
                            dItemInOrder.Delete(idToDelete);
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