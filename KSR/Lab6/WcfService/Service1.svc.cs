using System.IO;
using System.ServiceModel.Web;
using System.Xml;

namespace WcfService
{
    public class Service1 : IService1
    {
        public XmlDocument hostHTML()
        {
            var d = new XmlDocument();

            d.Load("C:\\Users\\Damian\\Studia\\Semestr_6\\KSR\\Lab6\\Debug\\index.xhtml");

            return d;
        }

        public Stream hostJS()
        {
            var script =  new FileStream("C:\\Users\\Damian\\Studia\\Semestr_6\\KSR\\Lab6\\Debug\\scripts.js", FileMode.Open);

            WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";

            return script;

        }

        public int Dodaj(string a, string b)
        {
            return int.Parse(a) + int.Parse(b);
        }
    }
}
