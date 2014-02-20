using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Chat.Interfaces;

namespace Chat.Server.WCF
{
    public class ChatServer : IChatServer
    {
        private bool _disposed;
        private readonly ServiceHost _host;

        public ChatServer(Uri baseAddress)
        {
            _host = new ServiceHost(typeof (ChatService), baseAddress);
            _host.Description.Behaviors.Add(new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
            });
            _host.AddServiceEndpoint(typeof (IChatService), new WSDualHttpBinding(), "");
            _host.AddServiceEndpoint(
                  ServiceMetadataBehavior.MexContractName,
                  MetadataExchangeBindings.CreateMexHttpBinding(),
                  "mex"
                );
        }


        public void Start()
        {
            _host.Open();
        }

        public void Stop()
        {
            Dispose();
        }

        public IEnumerable<string> Endpoints { get { return _host.Description.Endpoints.Select(x=>x.Address.ToString()); } }
        
        public void Dispose()
        {
            if (_disposed) return;
            _host.Close();
            _disposed = true;
        }
    }
}
