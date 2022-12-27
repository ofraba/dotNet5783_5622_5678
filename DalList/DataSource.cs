using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using DalApi;
using DO;
namespace Dal;

static internal class DataSource
{
    static readonly Random _random = new Random();

    static internal class Config
    {
        static private int _order = 1;
        static public int Order
        {
            get { return _order++; }
        }

        static private int _itemInOrder = 0;
        static public int ItemInOrder
        {
            get { return _itemInOrder++; }
        }
    }

    static DataSource()
    {
        s_Initialize();
    }

    public static List<DO.Orders> orders=new List<DO.Orders>();
    public static List<DO.OrderItem> orderItems=new List<DO.OrderItem>();
    public static List<DO.Product> products=new List<DO.Product>();


    static void AddOrederItem(DO.OrderItem newOrderItem)
    {

        if(orderItems.Count<200)
        {
            orderItems.Add(newOrderItem);
        }
        else
        {
            Console.WriteLine("error");
        }
    }

    static void AddOrder(DO.Orders newOrder)
    {
        if(orders.Count<100)
        {
            orders.Add(newOrder);
        }
        else
        {
            Console.WriteLine("error");
        }
    }

    static void AddProduct(DO.Product newProduct)
    {
        if(products.Count<50)
        {
            products.Add(newProduct);
        }
        else
        {
            Console.WriteLine("error");
        }
    }

    static bool IsExsist(int id)
    {
        int i = 0;
        while (i < products.Count)
        {
            if (products[i].ID == id)
                return true;
            i++;
        }
        return false;
    }

    static private void s_Initialize()
    {                                                                                                                                                                                                              
        List<(string, string,Category)> products1 = new List<(string, string,Category)> {
            ("Set for 6 diners","gold",Category.dinnerware),
            ("Set of 6 glasses","gold",Category.dinnerware),
            ("Bath towel", "white",Category.textile),
            ("kitchen towel","yellow",Category.textile),
            ("Decorative tool for soap","pink",Category.textile),
            ("A luxurious vase","silver",Category.styling),
            ("Wool carpet","pink",Category.textile),
            ("Bedding with a speckled print","brown",Category.linen),
            ("Silk bedding","white",Category.linen),
            ("Fragrance distributor","blue",Category.bathAccessories)
        };
        

        for (int i = 0; i < 10; i++)
        {
            int price = Convert.ToInt32(_random.Next(50,700));
            int amount =Convert.ToInt32(_random.Next(1,25));
            int id = Convert.ToInt32(_random.Next(100000,999999));
            while(IsExsist(id))
            {
                id = Convert.ToInt32(_random.Next(100000,999999));
            }
            DO.Product product = new DO.Product { ID = id, Name = products1[i].Item1,Color= products1[i].Item2, Category = products1[i].Item3, Price = price, Amount= amount };
            AddProduct(product);
        }


         List<(string, string,string)> orders1 = new List<(string, string,string)> 
         {
            ("shany kravitz", "shany@gmail.com","bircat avraham"),
            ("chani mizrachi", "chani@gmail.com","mintz"),
            ("bat-chen dikman", "bat-chen@gmail.com","recanaty"),
            ("batsheva ben paz", "batsheva@gmail.com","ely hacohen"),
            ("tahila maimon", "tahila@gmail.com","bar ealan"),
            ("noa siton", "noa@gmail.com","rubin"),
            ("bitya shlezunger", "bitya@gmail.com","zarchi"),
            ("ariela druk", "ariel@gmail.com","truman"),
            ("yair dupark", "yair@gmail.com","ben zeev"),
            ("avishay mehudar", "avishay@gmail.com","idelzon"),
            ("neomi chadad","neomi@gmail.com","druk"),
            ("yael shitrit", "yael@gmail.com", "zolty"),
            ("tamar cohen","tamar@gmail.com","makor baruch"),
            ("hadasa levi","hadasa@gmail.com","brim"),
            ("efrat shiran","efrat@gmail.com","chazon eash"),
            ("ruth nachumy", "ruth@gmail.com","fatal"),
            ("michal ochayon","michal@gmail.com","mutzafy"),
            ("dvora tavill","dvora@gmail.com", "shtefenesh"),
            ("ayala rozner","ayala@gmail.com","toledano"),
            ("sara lavi", "sara@gmail.com","cahaneman")
        };



        for (int i = 0; i < 20; i++)
        {
            DateTime date = DateTime.Now;
            DateTime date2;
            DateTime date3;
            if (i < orders1.Count * 0.2)
            {
                date2 = DateTime.MinValue;
                date3 = DateTime.MinValue;
            }
            else
            {
                int shippingDay = (int)_random.Next(5, 10);
                TimeSpan dateSend = new TimeSpan(shippingDay, 0, 0, 0);
                date2 = date.Add(dateSend);
                if (i < orders1.Count * 0.2 + (orders1.Count * 0.8 * 0.6))
                {
                    int dayGet = (int)_random.Next(10, 14);
                    TimeSpan dateGet = new TimeSpan(dayGet, 0, 0, 0);
                    date3 = date.Add(dateGet);
                }
                else
                {
                    date3 = DateTime.MinValue;
                }
            }
            DO.Orders order = new DO.Orders { ID = Config.Order, CustomerName = orders1[i].Item1, CustomerEmail = orders1[i].Item2, CustomerAdress = orders1[i].Item3, OrderDate = date, ShipDate = date2, DeliveryDate = date3 };
            AddOrder(order);
        }

        // מגריל מוצרים למערך פריטים בהזמנה כך שבכל הזמנה יהיה בין 1 ל-4 מוצרים
        int [] arrMone=new int[20];
        int mone=0;//מונה את ההזמנות השונות שהוגרלו 
        for(int i = 0; i < 40; i++)
        {
            if ((40 - i) == (20 - mone))//אם נשאר מספר פריטים כמספר הזמנות שלא קיבלו פריט אז כל אחת מקבלת פריט אחד
            {
                for (int j = 0; j < 20; j++)
                {
                    if (arrMone[j] == 0)
                    {
                        int idOrder = j;
                        int idProduct = Convert.ToInt32(_random.Next(products.Count));
                        DO.OrderItem itemInOrder = new DO.OrderItem { ID = Config.ItemInOrder, OrderID = orders[idOrder].ID, ProductID = products[idProduct].ID, Price = products[idProduct].Price };
                        AddOrederItem(itemInOrder);
                    }
                }
                break;
            }
            else//מגרילים מוצר ואז מגרילים לו הזמנה
            {
                int idOrder = Convert.ToInt32(_random.Next(orders.Count));
                int idProduct = Convert.ToInt32(_random.Next(products.Count));
                arrMone[idOrder]++;
                if (arrMone[idOrder] == 1)
                    mone++;
                int amount = Convert.ToInt32(_random.Next(1, 20));
                DO.OrderItem itemInOrder = new DO.OrderItem { ID = Config.ItemInOrder, OrderID = orders[idOrder].ID, ProductID = products[idProduct].ID, Price = products[idProduct].Price, Amount= amount };
                AddOrederItem(itemInOrder);
            }
            
        }
    }
}

