using System;
using System.ServiceModel;

namespace Klient
{
    class Handler : ServiceReference1.IZadanie2Callback
    {
        public IAsyncResult BeginZadanie(string zadanie, int pkt, bool zaliczone, AsyncCallback callback, object asyncState)
        {
            return null;
        }

        public void EndZadanie(IAsyncResult result)
        {

        }

        public void Zadanie([MessageParameter(Name = "zadanie")] string zadanie1, int pkt, bool zaliczone)
        {
            Console.WriteLine("Zadanie: {0}, pkt: {1}, zaliczone: {2}", zadanie1, pkt, zaliczone);
        }
    }

    class Handler6 : ServiceReference2.IZadanie6Callback
    {
        public void Wynik(int wyn)
        {
            Console.WriteLine("Wynik: {0}", wyn);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var client1 = new ServiceReference1.Zadanie1Client();

            IAsyncResult res = client1.BeginDlugieObliczenia(null, null);

            for (int i = 0; i <= 20; i++)
            {
                client1.Szybciej(i, 3 * i * i - 2 * i);
            }


            Console.WriteLine(client1.EndDlugieObliczenia(res));


            var client2 = new ServiceReference1.Zadanie2Client(new InstanceContext(new Handler()));

            client2.PodajZadania();

            var client3 = new ServiceReference2.Zadanie5Client();

            Console.WriteLine(client3.ScalNapisy("Ala", "ma kota"));

            var client4 = new ServiceReference2.Zadanie6Client(new InstanceContext(new Handler6()));

            client4.Dodaj(3, 5);

            Console.ReadKey();

            ((IDisposable)client1).Dispose();
            ((IDisposable)client2).Dispose();
            ((IDisposable)client3).Dispose();
            ((IDisposable)client4).Dispose();

        }
    }
}
