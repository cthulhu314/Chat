using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Interfaces
{
    public interface IChatClient<TMessage> : IDisposable
    {
        event EventHandler<TMessage> OnMessageRecieved;
        void Send(TMessage message);
    }
}
