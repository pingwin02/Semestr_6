using System;
using System.ServiceModel;
using Zad6;

namespace Zad7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fact = new ChannelFactory<IZadanie7>(new NetNamedPipeBinding(),
                        new EndpointAddress("net.pipe://localhost/ksr-wcf1-zad6"));

            var client = fact.CreateChannel();

            try
            {
                client.RzucWyjatek7("Ala", 2);
            }
            catch (FaultException<Wyjatek7> e)
            {
                Console.WriteLine("Wyjątek: " + e.Detail);
            }



            ((IDisposable)client).Dispose();

            fact.Close();
        }
    }
}
