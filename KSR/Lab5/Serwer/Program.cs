using KSR_WCF2;
using System;
using System.ServiceModel;

namespace Serwer
{
    public class Zadanie3 : IZadanie3
    {
        public void TestujZwrotny()
        {
            for (int i = 0; i <= 30; i++)
            {
                var zwr = OperationContext.Current.GetCallbackChannel<IZadanie3Zwrotny>();
                zwr.WolanieZwrotne(i, i * i * i - i * i);
            }
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Zadanie4 : IZadanie4
    {
        private int licznik;
        public int Dodaj(int v)
        {
            licznik += v;
            return licznik;
        }

        public void Ustaw(int v)
        {
            licznik = v;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var host1 = new ServiceHost(typeof(Zadanie3));

            host1.AddServiceEndpoint(typeof(IZadanie3),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf2-zad3");

            host1.Open();
            Console.WriteLine("Serwer1 działa");

            var host2 = new ServiceHost(typeof(Zadanie4));

            host2.AddServiceEndpoint(typeof(IZadanie4),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf2-zad4");

            host2.Open();
            Console.WriteLine("Serwer2 działa");

            Console.ReadKey();
            host1.Close();
            host2.Close();

        }
    }
}
