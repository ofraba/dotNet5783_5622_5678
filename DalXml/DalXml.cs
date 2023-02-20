using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        public static IDal Instance { get; } = new DalXml();

        public IProduct Product { get; } = new Dal.Product();
        public IOrder Order { get; } = new Dal.Orders();
        public IOrderItem OrderItem { get; } = new Dal.OrderItems();
        private DalXml() { }
    }
}
