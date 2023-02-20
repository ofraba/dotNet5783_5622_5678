using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal
{
    internal class Orders : IOrder
    {
        public int Add(DO.Orders o)
        {
            XElement? config = XDocument.Load("../config.xml").Root;
            XElement? idElement = config?.Element("OrderId");
            int id = Convert.ToInt32(idElement.Value);//
            o.ID = id++;
            idElement.Value = id.ToString();
            List<DO.Orders> lst1 = new();
            //= GetAll().ToList();
            lst1.Add(o);
            StreamWriter write = new StreamWriter("../Orders.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Orders>));
            ser.Serialize(write, lst1);
            write.Close();
            return o.ID;
        }

        public DO.Orders Get(int id)
        {
            List<DO.Orders> lst1 = GetAll().ToList();
            return lst1.Find(o => o.ID == id);
        }

        public DO.Orders Get(Predicate<DO.Orders> func)
        {
            List<DO.Orders> lst1 = GetAll().ToList();
            return lst1.Find(func);
        }
        public IEnumerable<DO.Orders> GetAll(Func<DO.Orders, bool>? func = null)
        {
            StreamReader r = new StreamReader("../Orders.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Orders>));
            List<DO.Orders> lst = new List<DO.Orders> { };
            lst = (List<DO.Orders>)ser.Deserialize(r);
            r.Close();
            return func == null ? lst : lst.Where(func);
        }
        public void Delete(int id)
        {
            List<DO.Orders> lst = GetAll().ToList();
            DO.Orders order = lst.Find(o => o.ID == id);
            lst.Remove(order);
            StreamWriter w = new StreamWriter("../Orders.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Orders>));
            ser.Serialize(w, lst);
            w.Close();
        }
        public void Update(DO.Orders o)
        {
            Delete(o.ID);
            Add(o);
        }
    }
}
