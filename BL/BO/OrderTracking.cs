using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus status { get; set; }
    public List<(DateTime, string)> dateAndDescription { get; set; }
    //public List<DateTime,string> dateAndDescription { get; set; }  
    public override string ToString() => $@"
ID = {ID}: 
status: {status}
dateAndDescription: {dateAndDescription}
";
}
