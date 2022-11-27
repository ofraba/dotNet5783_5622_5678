using BLApi;
using BO;
using BL;
using BLApi;
using BlImplementation;

namespace BlTest
{
    internal class Program
    {
        static private IBl bl = new Bl();
        static void Main(string[] args)
        {
            Console.WriteLine("enter 0 for exit,\n 1 for product, \n 2 for orders, \n 3 for cart:");
            int number = int.Parse(Console.ReadLine());
            while (number != 0)
            {
                switch (number)
                {
                    case 1:
                        Console.WriteLine("enter a for show all of the products,\n b for show the product for catalog,\n c for show product for manager, \n d for show product for client, \n e for update,\n f for delete:");
                        char pChoose = Convert.ToChar(Console.ReadLine());
                        productFunction(pChoose);
                        break;
                    case 2:
                        Console.WriteLine("enter a for show all of the orders,\n b for show the order for manager,\n c for update Order Shipping, \n d for update Order Delivery:");
                        char oChoose = Convert.ToChar(Console.ReadLine());
                        ordersFunction(oChoose);
                        break;
                    case 3:
                        Console.WriteLine("enter a for add product to the cart,\n b for update amont,\n c for confirm order:");
                        char cChoose = Convert.ToChar(Console.ReadLine());
                        cartFunction(cChoose);
                        break;
                    default:
                        break;
                }
            }
        }

        static void productFunction(char pChoose)
        {
            int id;
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
                case 'c':
                    Console.WriteLine("enter id of product you want to show");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(bl.Product.GetForManegar(id));
                    }
                    catch
                    {
                        //זורק חריגה
                    }
                    break;
                case 'd':
                    Console.WriteLine("enter id of product you want to show");
                    id = int.Parse(Console.ReadLine());
                    try
                    {

                        Console.WriteLine(bl.Product.GetForClient(id,));
                    }
                    catch
                    {
                        //זורק חריגה
                    }
                    break;
                default:
                    break;

            }
        }

        static void ordersFunction(char oChoose)
        {

        }

        static void cartFunction(char cChoose)
        {

        }

    }
}