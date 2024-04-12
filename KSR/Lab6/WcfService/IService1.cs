using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml;

namespace WcfService
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract, WebGet(UriTemplate = "index.html"), XmlSerializerFormat]
        XmlDocument hostHTML();

        [OperationContract, WebGet(UriTemplate = "scripts.js")]
        Stream hostJS();

        [OperationContract, WebInvoke(UriTemplate = "Dodaj/{a}/{b}")]
        int Dodaj(string a, string b);
    }
}
