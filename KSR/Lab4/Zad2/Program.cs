using KSR_WCF1;
using System;
using System.ServiceModel;

namespace Zad2
{
    public class Zadanie2 : IZadanie2
    {
        string IZadanie2.Test(string arg)
        {
            Console.WriteLine("Wywołano metodę Test z argumentem: " + arg);
            return "Test: " + arg;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie2));
            host.AddServiceEndpoint(typeof(IZadanie2), new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad2");

            host.Open();
            Console.WriteLine("Host started");
            Console.ReadKey();
            host.Close();
        }
    }
}
