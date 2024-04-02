using KSR_WCF1;
using System;
using System.ServiceModel;

namespace Zad5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fact = new ChannelFactory<IZadanie1>(new NetNamedPipeBinding(),
                        new EndpointAddress("net.pipe://localhost/ksr-wcf1-test"));

            var client = fact.CreateChannel();

            try
            {
                client.RzucWyjatek(true);

            }
            catch (FaultException<Wyjatek> e)
            {
                Console.WriteLine("Wyjątek: " + e.Detail);
            }


            fact.Close();
        }
    }
}
