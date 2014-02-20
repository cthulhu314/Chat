using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chat.Server.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {

        private readonly BlockingCollection<IChatCallback> _clients = new BlockingCollection<IChatCallback>(); 

        public void Register()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            if(!_clients.Contains(callback))
                _clients.Add(callback);
        }

        public void Send(string message)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            foreach (var chatCallback in _clients.Where(x => x != callback))
            {
                chatCallback.NotifyBroadcast(message);
            }
        }

        public void UnRegister()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();
            _clients.TryTake(out callback);
        }
    }
}
