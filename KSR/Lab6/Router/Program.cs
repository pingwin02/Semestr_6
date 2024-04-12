using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Routing;

namespace Router
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var routeTo1 = "net.tcp://localhost:1/zad6";
            var routeTo2 = "net.tcp://localhost:2/zad6";

            var routerAddr = "net.tcp://localhost:3/zad6";

            var h = new ServiceHost(typeof(RoutingService));
            h.AddServiceEndpoint(typeof(IRequestReplyRouter),
                new NetTcpBinding(),
                routerAddr);

            var rc = new RoutingConfiguration();
            var contract = ContractDescription.GetContract(typeof(IRequestReplyRouter));

            var lst = new List<ServiceEndpoint>();

            var client1 = new ServiceEndpoint(contract, new NetTcpBinding(),
                new EndpointAddress(routeTo1));
            var client2 = new ServiceEndpoint(contract, new NetTcpBinding(),
                new EndpointAddress(routeTo2));
            lst.Add(client1);
            lst.Add(client2);

            rc.FilterTable.Add(new MatchAllMessageFilter(), lst);

            h.Description.Behaviors.Add(new RoutingBehavior(rc));

            h.Open();
            Console.WriteLine("Router uruchomiony");
            Console.ReadKey();
            h.Close();

        }
    }
}
