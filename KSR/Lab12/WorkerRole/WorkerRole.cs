using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        private CloudStorageAccount account;
        private CloudQueueClient queueClient;
        private CloudBlobClient blobClient;

        private CloudQueue doZakodowaniaQueue;

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Use TLS 1.2 for Service Bus connections
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            account = CloudStorageAccount.DevelopmentStorageAccount;
            queueClient = account.CreateCloudQueueClient();
            blobClient = account.CreateCloudBlobClient();

            doZakodowaniaQueue = queueClient.GetQueueReference("dozakodowania");
            doZakodowaniaQueue.CreateIfNotExists();

            Trace.TraceInformation("WorkerRole has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var nazwa = doZakodowaniaQueue.GetMessage(visibilityTimeout: new System.TimeSpan(0, 0, 5));

                if (nazwa == null)
                {
                    Trace.TraceInformation("Brak zadañ do wykonania");
                    await Task.Delay(1000);
                }
                else
                {
                    Trace.TraceInformation("Kodowanie pliku {0}", nazwa.AsString);
                    if (new System.Random().Next(1, 3) == 1)
                    {
                        Trace.TraceError("Kodowanie nie powiod³o siê");
                        throw new System.Exception("Kodowanie nie powiod³o siê");
                    }

                    var sourceContainer = blobClient.GetContainerReference("jawne");
                    sourceContainer.CreateIfNotExists();

                    var targetContainer = blobClient.GetContainerReference("zakodowane");
                    targetContainer.CreateIfNotExists();

                    var blob = sourceContainer.GetBlockBlobReference(nazwa.AsString);
                    var s = new System.IO.MemoryStream();
                    blob.DownloadToStream(s);
                    string content = System.Text.Encoding.UTF8.GetString(s.ToArray());

                    var bytes = new System.Text.ASCIIEncoding().GetBytes(content);
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        if (bytes[i] >= 'a' && bytes[i] <= 'z')
                        {
                            bytes[i] = (byte)((bytes[i] - 'a' + 13) % 26 + 'a');
                        }
                        else if (bytes[i] >= 'A' && bytes[i] <= 'Z')
                        {
                            bytes[i] = (byte)((bytes[i] - 'A' + 13) % 26 + 'A');
                        }
                    }

                    var s2 = new System.IO.MemoryStream(bytes);
                    blob = targetContainer.GetBlockBlobReference(nazwa.AsString);
                    blob.UploadFromStream(s2);
                    doZakodowaniaQueue.DeleteMessage(nazwa);

                    Trace.TraceInformation("Zakoñczono kodowanie pliku {0}", nazwa.AsString);
                }

            }
        }
    }
}
