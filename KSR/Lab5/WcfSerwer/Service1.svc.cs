using KSR_WCF2;
using System.ServiceModel;

namespace WcfSerwer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IZadanie5, IZadanie6
    {
        public string ScalNapisy(string a, string b)
        {
            return a + b;
        }

        public void Dodaj(int a, int b)
        {
            var zwr = OperationContext.Current.GetCallbackChannel<IZadanie6Zwrotny>();
            zwr.Wynik(a + b);
        }
    }
}

// test.exe http://localhost:12345/Service1.svc/zad5 http://localhost:12345/Service1.svc/zad6