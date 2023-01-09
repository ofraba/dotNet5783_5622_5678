using BLApi;
using BO;
using BL;
using BlImplementation;
using DO;
using Dal;
using DalApi;
using Microsoft.VisualBasic;

namespace BlTest
{
    internal class Program
    {
        static private IBl bl = new Bl();
        static readonly Random random = new Random();
        static void Main(string[] args)
        {
            BO.Cart cart = new BO.Cart();
            cart.Items = new List<BO.OrderItem>();
            Console.WriteLine("enter 0 for exit,\n 1 for product, \n 2 for orders, \n 3 for cart:");
            int number;
            int.TryParse(Console.ReadLine(), out number);
            while (number != 0)
            {
                switch (number)
                {
                    case 1:
                        Console.WriteLine("enter a for show all of the products,\n b for show the products for catalog,\n c for show details of product, \n d for add product \n e for update\n f for delete:");
                        char pChoose;
                        char.TryParse(Console.ReadLine(), out pChoose);
                        productFunction(pChoose);
                        break;
                    case 2:
                        Console.WriteLine("enter a for show all of the orders,\n b for show the order for manager,\n c for update Order Shipping, \n d for update Order Delivery:");
                        char oChoose;
                        char.TryParse(Console.ReadLine(),out oChoose);
                        ordersFunction(oChoose);
                        break;
                    case 3:
                        Console.WriteLine("enter a for add product to the cart,\n b for update amont,\n c for confirm order:");
                        char cChoose;
                        char.TryParse(Console.ReadLine(), out cChoose);
                        cartFunction(cChoose, cart);
                        break;
                    default:
                        break;
                }
                Console.WriteLine("enter 0 for exit,\n 1 for product, \n 2 for orders, \n 3 for cart:");
                int.TryParse(Console.ReadLine(), out number);
            }
        }

        static void productFunction(char pChoose)
        {
            int id, productCategory;
            double tmp;
            int tmp1;
            switch (pChoose)
            {
                case 'a':
                    IEnumerable<ProductForList> products = bl.Product.GetAll();
                    foreach (ProductForList product in products)
                    {
                        Console.WriteLine(product);
                    }
                    break;
                case 'b':
                    IEnumerable<ProductItem> products1 = bl.Product.GetForCatalog();
                    foreach (ProductItem product1 in products1)
                    {
                        Console.WriteLine(product1);
                    }
                    break;
                case 'c'://get details of product
                    Console.WriteLine("enter id of product you want to show");
                    int.TryParse(Console.ReadLine(), out id);
                    try
                    {
                        Console.WriteLine(bl.Product.GetForManegar(id));
                    }
                    catch (BO.ExceptionFromDal e)
                    {
                        Console.WriteLine(e.Message + " " + e.InnerException?.Message);
                    }
                    break;
                case 'd'://add a product
                    BO.Product newProduct = new BO.Product();
                    newProduct.ID = Convert.ToInt32(random.Next(100000, 999999));
                    Console.WriteLine("enter name of product");
                    newProduct.Name = Console.ReadLine();
                    Console.WriteLine("enter price of the product");
                    double.TryParse(Console.ReadLine(), out tmp);
                    newProduct.Price = tmp;
                    Console.WriteLine("enter color of the product");
                    newProduct.Color = Console.ReadLine();
                    Console.WriteLine("enter category\n 1 for dinnerware\n 2 for linen\n 3 for bathAccessories\n 4 for styling\n 5 for textile");
                    int.TryParse(Console.ReadLine(), out tmp1);
                    productCategory = tmp1;
                    newProduct.Category = (BO.Category)productCategory;
                    Console.WriteLine("enter amount of the product");
                    int.TryParse(Console.ReadLine(), out tmp1);
                    newProduct.InStock = tmp1;
                    try
                    {
                        Console.WriteLine(bl.Product.Add(newProduct));
                    }
                    catch (BO.dataIsntInvalid e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (BO.ExceptionFromDal e)
                    {
                        Console.WriteLine(e.Message + " " + e.InnerException?.Message);
                    }
                    break;
                case 'e'://update product
                    BO.Product updateProduct = new BO.Product();
                    Console.WriteLine("enter id of product");
                    int.TryParse(Console.ReadLine(), out tmp1);
                    updateProduct.ID = tmp1;
                    Console.WriteLine("enter name of product");
                    updateProduct.Name = Console.ReadLine();
                    Console.WriteLine("enter price of the product");
                    double.TryParse(Console.ReadLine(), out tmp);
                    updateProduct.Price = tmp;
                    Console.WriteLine("enter color of the product");
                    updateProduct.Color = Console.ReadLine();
                    Console.WriteLine("enter category\n 1 for dinnerware\n 2 for linen\n 3 for bathAccessories\n 4 for styling\n 5 for textile");
                    int.TryParse(Console.ReadLine(), out tmp1);
                    productCategory = tmp1;
                    updateProduct.Category = (BO.Category)productCategory;
                    Console.WriteLine("enter amount of the product");
                    int.TryParse(Console.ReadLine(), out tmp1);
                    updateProduct.InStock = tmp1;
                    try
                    {
                        bl.Product.Update(updateProduct);
                    }
                    catch (BO.ExceptionFromDal e)
                    {
                        Console.WriteLine(e.Message + " " + e.InnerException?.Message);
                    }
                    break;
                case 'f'://delete product
                    Console.WriteLine("enter id of product");
                    int.TryParse(Console.ReadLine(), out id);
                    try
                    {
                        bl.Product.Delete(id);
                    }
                    catch (ExceptionFromDal e)
                    {
                        Console.WriteLine(e.Message + " " + e.InnerException?.Message);
                    }
                    catch (productExsistInOrder e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                default:
                    break;

            }
        }

        static void ordersFunction(char oChoose)
        {
            int orderId;
            switch (oChoose)
            {
                case 'a'://show all of the orders
                    IEnumerable<BO.OrderForList> listOfOrders = bl.Order.GetAll();
                    foreach (var item in listOfOrders)
                        Console.WriteLine(item);
                    break;
                case 'b'://show the order for manager
                    Console.WriteLine("enter id of order you want to show");
                    int.TryParse(Console.ReadLine(), out orderId);
                    try
                    {
                        Console.WriteLine(bl.Order.GetForManegar(orderId));
                    }
                    catch (negativeIDnumber e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (ExceptionFromDal e)
                    {
                        Console.WriteLine(e.Message + " " + e.InnerException?.Message);
                    }
                    break;
                case 'c'://update Order Shipping
                    Console.WriteLine("enter id of order you want to update shipping");
                    int.TryParse(Console.ReadLine(),out orderId);
                    try
                    {
                        Console.WriteLine(bl.Order.OrderShippingUpdate(orderId));
                    }
                    catch (ExceptionFromDal e)
                    {
                        Console.WriteLine(e.Message + " " + e.InnerException?.Message);
                    }
                    catch (TheOrderHasBeenSent e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 'd'://update Order Delivery
                    Console.WriteLine("enter id of order you want to update delivery");
                    int.TryParse(Console.ReadLine(), out orderId);
                    try
                    {
                        Console.WriteLine(bl.Order.OrderDeliveryUpdate(orderId));
                    }
                    catch (ExceptionFromDal e)
                    {
                        Console.WriteLine(e.Message + " " + e.InnerException?.Message);
                    }
                    catch (orderHasBeenDelivered e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        static void cartFunction(char cChoose, BO.Cart cart)
        {
            int id;
            switch (cChoose)
            {
                case 'a'://add product to the cart
                    Console.WriteLine("enter id of product you want to add to the cart");
                    int.TryParse(Console.ReadLine(), out id);
                    try
                    {
                        BO.Cart cart2 = bl.Cart.AddProductToCart(cart, id);
                        Console.WriteLine(cart2);
                        foreach (var item in cart2.Items)
                            Console.WriteLine(item);    

                    }
                    catch (ExceptionFromDal e)
                    {
                        Console.WriteLine(e.Message + " " + e.InnerException?.Message);
                    }
                    catch (notEnoughAmount e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 'b'://update amount
                    Console.WriteLine("enter id of product you want to update");
                    int.TryParse(Console.ReadLine(),out id);
                    Console.WriteLine("enter new amount");
                    int amount;
                    int.TryParse(Console.ReadLine(),out amount);
                    try
                    {
                        Console.WriteLine(bl.Cart.Update(cart, id, amount));
                    }
                     catch (notEnoughAmount e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 'c'://confirm order
                    Console.WriteLine("enter customer's email");
                    string? email = Console.ReadLine();
                    Console.WriteLine("enter customer's name");
                    string? name = Console.ReadLine();
                    Console.WriteLine("enter customer's address");
                    string? address = Console.ReadLine();
                    try
                    {
                        bl.Cart.Confirm(cart, name, email, address);
                    }
                    catch (ExceptionFromDal e)
                    {
                        Console.WriteLine(e.Message + " " + e.InnerException?.Message);
                    }
                    catch (dataIsntInvalid e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                default:
                    break;
            }
        }

    }
}