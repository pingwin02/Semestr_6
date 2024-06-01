using System.ServiceModel;

namespace WCFServiceWebRole
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        void Koduj(string nazwa, string tresc);

        [OperationContract]
        string Pobierz(string nazwa);

    }
}
