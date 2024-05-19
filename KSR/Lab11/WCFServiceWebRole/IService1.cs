using System;
using System.ServiceModel;

namespace WCFServiceWebRole
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        void Create(string login, string password);
        [OperationContract]
        Guid Login(string login, string password);
        [OperationContract]
        void Logout(string login);
        [OperationContract]
        void Put(string name, string value, Guid sessionId);
        [OperationContract]
        string Get(string name, Guid sessionId);

    }
}
