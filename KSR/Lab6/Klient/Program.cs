using Serwer;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using System.ServiceModel.Web;

namespace Klient
{
    [ServiceContract]
    public interface IUsluga
    {
        [OperationContract, WebInvoke(UriTemplate = "Dodaj/{a}/{b}")]
        int Dodaj(string a, string b);
    }

    [ServiceContract]
    public interface IZadanie6
    {
        [OperationContract]
        int Dodaj(int a, int b);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DiscoveryClient discoveryClient = new DiscoveryClient(
                new UdpDiscoveryEndpoint("soap.udp://localhost:30703"));

            System.Collections.ObjectModel.Collection<EndpointDiscoveryMetadata> lst =
                discoveryClient.Find(new FindCriteria(typeof(IZadanie1))).Endpoints;

            discoveryClient.Close();

            if (lst.Count > 0)
            {
                var addr = lst[0].Address;
                var proxy = ChannelFactory<IZadanie1>.CreateChannel(new BasicHttpBinding(), addr);
                Console.WriteLine(proxy.ScalNapisy("Ala", " ma kota"));
                ((IDisposable)proxy).Dispose();
            }

            var f = new ChannelFactory<IUsluga>(new WebHttpBinding(),
            new EndpointAddress("http://localhost:12345/Service1.svc/Zad3"));
            f.Endpoint.Behaviors.Add(new WebHttpBehavior());
            var c = f.CreateChannel();
            Console.WriteLine(c.Dodaj("2", "3"));
            ((IDisposable)c).Dispose();

            var f2 = new ChannelFactory<IZadanie6>(new NetTcpBinding(), 
                new EndpointAddress("net.tcp://localhost:3/zad6"));

            var c2 = f2.CreateChannel();

            Console.WriteLine(c2.Dodaj(2, 3));

            ((IDisposable)c2).Dispose();
        }
    }
}
