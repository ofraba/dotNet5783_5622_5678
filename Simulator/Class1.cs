using BLApi;
using BO;
using BlImplementation;
using DalApi;
using BL;

namespace Simulator
{
    public class Class1
    {

        IBl? bl = BLApi.Factory.Get();
        public static void Run()
        {
            Thread t = new Thread(DoTheSimulator);
            t.Start();
        }
        public static void DoTheSimulator()
        {
           
        }
    }
}