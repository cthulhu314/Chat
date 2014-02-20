using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chat.Server.WCF
{
    [ServiceContract(CallbackContract = typeof(IChatCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true)]
        void Register();
        [OperationContract(IsOneWay = true)]
        void Send(string body);
        [OperationContract(IsOneWay = true)]
        void UnRegister();
    }
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyBroadcast(string body);
    }
}
