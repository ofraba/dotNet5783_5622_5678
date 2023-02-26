using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal
{
    internal class OrderItems : IOrderItem
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.OrderItem ot)
        {
            XElement? config = XDocument.Load("../config.xml").Root;
            XElement? idElement = config?.Element("OrderItemId");
            int id = Convert.ToInt32(idElement?.Value);
            ot.ID = id++;
            idElement.Value = id.ToString();
            config?.Save("../config.xml");
            List<DO.OrderItem> lst1 = GetAll().ToList();
            lst1.Add(ot);
            StreamWriter write = new StreamWriter("../OrderItem.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
            ser.Serialize(write, lst1);
            write.Close();
            return ot.ID;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.OrderItem Get(int id)
        {
            List<DO.OrderItem> lst1 = GetAll().ToList();
            return lst1.Find(ot => ot.ID == id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.OrderItem Get(Predicate<DO.OrderItem> func)
        {
            List<DO.OrderItem> lst1 = GetAll().ToList();
            return lst1.Find(func);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
        {
            StreamReader r = new StreamReader("../OrderItem.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
            List<DO.OrderItem> lst = new List<DO.OrderItem> { };
            lst = (List<DO.OrderItem>)ser.Deserialize(r);
            r.Close();
            return func == null ? lst : lst.Where(func);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            List<DO.OrderItem> lst = GetAll().ToList();
            DO.OrderItem order = lst.Find(ot => ot.ID == id);
            lst.Remove(order);
            StreamWriter w = new StreamWriter("../Orders.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Orders>));
            ser.Serialize(w, lst);
            w.Close();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(DO.OrderItem ot)
        {
            Delete(ot.ID);
            Add(ot);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<OrderItem> FindAllOrderItem(int idOrder)
        {
            List<DO.OrderItem> lst1 = GetAll().ToList();
            return lst1.Where(o => o.OrderID == idOrder);
        }
    }
}

