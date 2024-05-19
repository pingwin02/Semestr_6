using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace WCFServiceWebRole
{
    public class Uzytkownik : TableEntity
    {
        public Uzytkownik(string rk, string pk)
        {
            this.PartitionKey = pk;
            this.RowKey = rk;
        }

        public Uzytkownik() { }

        public string Login { get; set; }
        public string Password { get; set; }
        public Guid SessionId { get; set; }
    }
}