using System;
using System.ServiceModel;

namespace Serwis
{
    [ServiceContract]
    public interface IZadanie6
    {
        [OperationContract]
        int Dodaj(int a, int b);
    }
    public class Zadanie6 : IZadanie6
    {
        public int Dodaj(int a, int b)
        {
            Console.WriteLine($"Serwis: {a} + {b}");
            return a + b;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var number = args[args.Length - 1];

            var host = new ServiceHost(typeof(Zadanie6));
            host.AddServiceEndpoint(typeof(IZadanie6), 
                new NetTcpBinding(), new Uri($"net.tcp://localhost:{number}/zad6"));
            host.Open();
            Console.WriteLine($"Serwis {number} uruchomiony");
            Console.ReadKey();
            host.Close();
        }
    }
}
