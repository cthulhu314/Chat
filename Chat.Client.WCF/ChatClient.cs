using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Chat.Client.WCF.Client;
using Chat.Interfaces;

namespace Chat.Client.WCF
{
    public class ChatClient : IChatClient<string>
    {
        private class ChatClientCallback : IChatServiceCallback
        {
            private readonly ChatClient _client;

            public ChatClientCallback(ChatClient client)
            {
                _client = client;
            }

            public void NotifyBroadcast(string body)
            {
                if (_client.OnMessageRecieved != null)
                    _client.OnMessageRecieved(_client,body);
            }
        }

        private readonly ChatServiceClient _client;

        public ChatClient(string address)
        {
            _client = new ChatServiceClient(new InstanceContext(new ChatClientCallback(this)),new WSDualHttpBinding(),new EndpointAddress(address));
            _client.Register();
        }

        public void Send(string message)
        {
            _client.Send(message);
        }

        public void Dispose()
        {
            _client.UnRegister();
            _client.Close();
        }

        public event EventHandler<string> OnMessageRecieved;
    }
}
