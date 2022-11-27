using System.Collections.Generic;
using DalApi;
using DO;
namespace Dal;

static internal class DataSource
{
    static readonly Random _random = new Random();

    static internal class Config
    {
        static private int _order = 0;
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
        List<(string, string,string)> products1 = new List<(string, string,string)> {
            ("Set for 6 diners","gold","dinnerware"),
            ("Set of 6 glasses","gold","dinnerware"),
            ("Bath towel", "white","textile"),
            ("kitchen towel","yellow","textile"),
            ("Decorative tool for soap","pink","textile"),
            ("A luxurious vase","silver","stywling"),
            ("Wool carpet","pink","textile"),
            ("Bedding with a speckled print","brown","linen"),
            ("Silk bedding","white","linen"),
            ("Fragrance distributor","blue","bathAccessories")
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
            DateTime date1 = DateTime.MinValue;
            DateTime date2 = DateTime.MinValue;
            int daySend = Convert.ToInt32(_random.Next(5,10));
            int dayGet = Convert.ToInt32(_random.Next(5,10));
            TimeSpan dateSend = new TimeSpan(daySend, 0, 0, 0);
            TimeSpan dateGet = new TimeSpan(dayGet, 0, 0, 0);
            DO.Orders order = new DO.Orders { ID = Config.Order, CustomerName = orders1[i].Item1, CustomerEmail = orders1[i].Item2, CustomerAdress = orders1[i].Item3, OrderDate = date, ShipDate = date1.Add(dateSend), DeliveryDate = date2.Add(dateGet) };
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
                DO.OrderItem itemInOrder = new DO.OrderItem { ID = Config.ItemInOrder, OrderID = orders[idOrder].ID, ProductID = products[idProduct].ID, Price = products[idProduct].Price };
                AddOrederItem(itemInOrder);
            }
            
        }
    }
}

