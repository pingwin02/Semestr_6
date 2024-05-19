using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;

namespace WCFServiceWebRole
{
    public class Service1 : IService1
    {
        public void Create(string login, string password)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference("uzytkownicy");
            table.CreateIfNotExists();

            var uzytkownik = new Uzytkownik(login, login)
            {
                Login = login,
                Password = password,
            };

            TableOperation op = TableOperation.Insert(uzytkownik);
            table.Execute(op);
        }

        public Guid Login(string login, string password)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference("uzytkownicy");
            table.CreateIfNotExists();

            TableOperation op = TableOperation.Retrieve<Uzytkownik>(login, login);
            var res = table.Execute(op);

            if (res.Result == null)
            {
                throw new Exception("Niepoprawne dane logowania");
            }

            var uzytkownik = (Uzytkownik)res.Result;
            if (uzytkownik.Password != password)
            {
                throw new Exception("Niepoprawne dane logowania");
            }

            uzytkownik.SessionId = Guid.NewGuid();

            op = TableOperation.Replace(uzytkownik);
            table.Execute(op);

            return uzytkownik.SessionId;
        }

        public void Logout(string login)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference("uzytkownicy");
            table.CreateIfNotExists();

            TableOperation op = TableOperation.Retrieve<Uzytkownik>(login, login);
            var res = table.Execute(op);

            if (res.Result == null)
            {
                throw new Exception("Niepoprawne dane logowania");
            }

            var uzytkownik = (Uzytkownik)res.Result;
            uzytkownik.SessionId = Guid.Empty;

            op = TableOperation.Replace(uzytkownik);
            table.Execute(op);
        }

        public void Put(string name, string value, Guid sessionId)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference("uzytkownicy");
            table.CreateIfNotExists();

            TableQuery<Uzytkownik> query =
                new TableQuery<Uzytkownik>().Where(
                               TableQuery.GenerateFilterConditionForGuid("SessionId",
                               QueryComparisons.Equal,
                               sessionId)
                               );

            var res = table.ExecuteQuery(query);

            if (res.Count() == 0)
            {
                throw new Exception("Niepoprawna sesja");
            }

            var login = res.First().Login;

            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(login + "-pliki");
            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(name);

            var bytes = new System.Text.ASCIIEncoding().GetBytes(value);
            var s = new System.IO.MemoryStream(bytes);
            blob.UploadFromStream(s);
        }

        public string Get(string name, Guid sessionId)
        {
            var account = CloudStorageAccount.DevelopmentStorageAccount;
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference("uzytkownicy");
            table.CreateIfNotExists();

            TableQuery<Uzytkownik> query =
                new TableQuery<Uzytkownik>().Where(
                               TableQuery.GenerateFilterConditionForGuid("SessionId",
                               QueryComparisons.Equal,
                               sessionId)
                               );

            var res = table.ExecuteQuery(query);

            if (res.Count() == 0)
            {
                throw new Exception("Niepoprawna sesja");
            }

            var login = res.First().Login;

            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(login + "-pliki");
            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(name);

            if (!blob.Exists())
            {
                throw new Exception("Nie ma takiego pliku");
            }

            var s = new System.IO.MemoryStream();
            blob.DownloadToStream(s);
            string content = new System.Text.ASCIIEncoding().GetString(s.ToArray());

            return content;
        }
    }
}
