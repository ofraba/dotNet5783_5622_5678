using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using Dal;
using DO; 
using DalApi;

namespace DalXml
{
    public class Class1
    {
        //Dal bl = new Dal();
        //public void f()
        //{
        //    XElement? elem = XDocument.Load("Product.xml").Root;
        //    foreach (var item in Bl.products.get())
        //    {
        //        XElement? e = new XElement("product");
        //        XElement? e1 = new XElement("Name", item.name);
        //        e.Add(e1);
        //        elem?.Add(e);
        //    }
        //    elem?.Save("products.xml"); // כל שינוי צריך  שמירה
        //}

        //public void get(int x)
        //{
        //    var products = XDocument.Load("products.xml").Root.Elements("product");
        //    products.Where(e => e.Element("name").Value == x);
        //    List<DO.Product> lst;
        //    products.ToList().ForEach(x => lst.Add(new Product() { name = x.Element("name").Value });
        //}

        //public void addSer()
        //{
        //    List<DO.Product> lst;//= ...;
        //    StreamWriter w = new StreamWriter("products.xml");
        //    XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));

        //    ser.Serialize(lst, w);

        //    StreamReader r = new StreamReader();

        //    lst = ser.Deserialize(r)
        //        w.Close();
        //}

    }
}



