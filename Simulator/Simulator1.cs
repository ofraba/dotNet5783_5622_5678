using BLApi;
using BO;
using BlImplementation;
using DalApi;
using BL;
using System.Threading;
using System.Threading.Channels;

namespace Simulator
{
    
    public static class Simulator1
    {
        public delegate void StatusChanged(Order order, string status, string newStatus, DateTime prev, DateTime next);
        public delegate void FinishSimulator(DateTime end, string reasonOfFinish);
        volatile private static bool stopSimulator = false;
        public static event StatusChanged? StatusChangedEvent = null;
        public static event FinishSimulator? FinishSimulatorEvent = null;
        public static bool IsAlive { get; set; } = false;
        public static void Run()
        {
            IsAlive = true;
            stopSimulator = false;
            Thread t = new Thread(DoTheSimulator);
            t.Start();
        }
        public static void DoTheSimulator()
        {
            string reasonOfFinish = "";
            IBl bl = BLApi.Factory.Get();
            Random rand = new Random();
            while (!stopSimulator)
            {
                int? id = bl.Order.ChooseOrder();
                if (id == null)
                {
                    stopSimulator = true;
                    reasonOfFinish = "There are no more treatment orders";
                    break;
                }
                int time = rand.Next(5000, 10000);
                int id1 = (int)id;
                try
                {
                    DateTime startChangeAt = DateTime.Now;
                    Order order = bl.Order.GetForManegar(id1);
                    string currentStatus, newStatus;
                    Thread.Sleep(time);
                    if (order.ShipDate == DateTime.MinValue)
                    {
                        currentStatus = "confirmed";
                        newStatus = "sent";
                        order.ShipDate = DateTime.Now;
                        bl.Order.OrderShippingUpdate(id1);
                    }
                    else
                    {
                        currentStatus = "sent";
                        newStatus = "provided";
                        order.DeliveryDate = DateTime.Now;
                        bl.Order.OrderDeliveryUpdate(id1);
                    }
                    
                    DateTime endChangeAt = DateTime.Now;
                   
                    StatusChangedEvent?.Invoke(order, currentStatus, newStatus, startChangeAt, endChangeAt);
                    Thread.Sleep(1000);
                }
                catch (ex1 e)
                {
                    throw new BO.ExceptionFromDal(e);
                }
            }
            FinishSimulatorEvent?.Invoke(DateTime.Now, reasonOfFinish);
        }
        public static void Stop()
        {
            stopSimulator = true;

            IsAlive= false;
        }

    }
}
