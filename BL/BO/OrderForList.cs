using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

public class OrderForList
{
    public int ID { get; set; }
    public string ?CustomerName { get; set; }
    public OrderStatus status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
ID = {ID}: 
customerName: {CustomerName},
status:{status},
amountOfItems: {AmountOfItems},
totalPrice: {TotalPrice}
";
}
