using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Zad6
{
    [ServiceContract]
    public interface IZadanie7
    {

        [OperationContract] [FaultContract(typeof(Wyjatek7))]
        void RzucWyjatek7(string a, int b);
    }

    [DataContract]
    public class Wyjatek7
    {
        [DataMember]
        public string opis { get; set; }
        [DataMember]
        public string a { get; set; }
        [DataMember]
        public int b { get; set; }
    }
    public class Zadanie7 : IZadanie7
    {
        public void RzucWyjatek7(string a, int b)
        {
            throw new FaultException<Wyjatek7>(new Wyjatek7()
            {
                opis = "Wyjątek7",
                a = a,
                b = b
            });
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie7));

            host.AddServiceEndpoint(typeof(IZadanie7), new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad6");


            host.Open();
            Console.WriteLine("Host started");
            Console.ReadKey();
            host.Close();
        }
    }
}
