using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal
{
    internal class Product : IProduct
    {
        //Dal bl = new Dal();
        //public void f()
        //{
        //    XElement? elem = XDocument.Load("Product.xml").Root;
        //    foreach (var item in bl.product.Get())
        //    {
        //        XElement? e = new XElement("product");
        //        XElement? e1 = new XElement("Name", item.name);
        //        e.Add(e1);
        //        elem?.Add(e);
        //    }
        //    elem?.Save("products.xml"); // כל שינוי צריך  שמירה
        //}

        //public DO.Product Get(int x)
        //{
        //    var products = XDocument.Load("products.xml").Root.Elements("product");
        //    products.Where(e => e.Element("name").Value == x);
        //    List<Product> lst;
        //    products.ToList().ForEach(x => lst.Add(new Product() { name = x.Element("name").Value });
        //}

        //public void Add()
        //{
        //    list<product> lst;//= ...;
        //    StreamWriter w = new StreamWriter("products.xml");
        //    XmlSerializer ser = new XmlSerializer(typeof(List<Product>));

        //    ser.Serialize(lst, w);

        //    StreamReader r = new StreamReader();

        //    lst = ser.Deserialize(r)
        //    w.Close();

        //}
    }
}
