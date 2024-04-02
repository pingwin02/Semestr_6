using KSR_WCF1;
using System;
using System.ServiceModel;

namespace Zad1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fact = new ChannelFactory<IZadanie1>(new NetNamedPipeBinding(),
                        new EndpointAddress("net.pipe://localhost/ksr-wcf1-test"));

            var client = fact.CreateChannel();

            client.Test("Zadanie 1 działa poprawnie!");

            ((IDisposable)client).Dispose();

            fact.Close();
        }
    }
}
