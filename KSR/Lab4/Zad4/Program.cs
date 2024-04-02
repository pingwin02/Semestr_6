using KSR_WCF1;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Zad4
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
            var b = host.Description.Behaviors.Find<ServiceMetadataBehavior>();

            host.AddServiceEndpoint(typeof(IZadanie2), new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad2");

            host.AddServiceEndpoint(typeof(IZadanie2), new NetTcpBinding(),
                "net.tcp://127.0.0.1:55765");

            if (b == null) b = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(b);

            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad2/mex");

            host.Open();
            Console.WriteLine("Host started");
            Console.ReadKey();
            host.Close();
        }
    }
}
