using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WCFServiceWebRole
{
    public class Service1 : IService1
    {
        public void Koduj(string nazwa, string tresc)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            var client = account.CreateCloudBlobClient();

            var container = client.GetContainerReference("jawne");
            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(nazwa);
            var bytes = new System.Text.ASCIIEncoding().GetBytes(tresc);
            var s = new System.IO.MemoryStream(bytes);
            blob.UploadFromStream(s);

            CloudQueueClient queueClient = account.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("dozakodowania");
            queue.CreateIfNotExists();

            var msg = new CloudQueueMessage(nazwa);
            queue.AddMessage(msg);
        }

        public string Pobierz(string nazwa)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            var client = account.CreateCloudBlobClient();

            var container = client.GetContainerReference("zakodowane");
            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(nazwa);
            var s = new System.IO.MemoryStream();
            blob.DownloadToStream(s);
            string content = System.Text.Encoding.UTF8.GetString(s.ToArray());

            return content;
        }
    }
}
