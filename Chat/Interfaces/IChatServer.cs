using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Interfaces
{
    public interface IChatServer : IDisposable
    {
        void Start();
        void Stop();
    }
}
