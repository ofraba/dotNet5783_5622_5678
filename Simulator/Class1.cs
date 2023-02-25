using BLApi;
using BO;
using BlImplementation;
using DalApi;
using BL;
using System.Threading;

namespace Simulator
{
    public delegate void StatusChanged(Order order, DateTime prev, DateTime next);
    public static class Class1
    {
       
        volatile private static bool stopSimulator = false;
        public static event StatusChanged? StatusChangedEvent = null;
        public static void Run()
        {

            Thread t = new Thread(DoTheSimulator);
            t.Start();
        }
        public static void DoTheSimulator()
        {
            IBl bl = BLApi.Factory.Get();
            Random rand = new Random();
            while (!stopSimulator)
            {
                int? id = bl.Order.ChooseOrder();
                if (id == null)
                {
                    stopSimulator = true;
                    break;
                }
                int time = rand.Next(5000, 10000);
                int id1 = (int)id;
                try
                {
                    DateTime startChangeAt = DateTime.Now;
                    Order order = bl.Order.GetForManegar(id1);
                    if (order.ShipDate == DateTime.MinValue)
                    {
                        order.ShipDate = DateTime.Now;
                        bl.Order.OrderShippingUpdate(id1);
                    }
                    else
                    {
                        order.DeliveryDate = DateTime.Now;
                        bl.Order.OrderDeliveryUpdate(id1);
                    }
                    Thread.Sleep(time);
                    DateTime endChangeAt = DateTime.Now;
                    if (StatusChangedEvent != null)
                        StatusChangedEvent(order, startChangeAt, endChangeAt);
                }
                catch (ex1 e)
                {
                    throw new BO.ExceptionFromDal(e);
                }
              
            }
        }
        public static void Stop()
        {
            stopSimulator = true;
        }

    }
}
