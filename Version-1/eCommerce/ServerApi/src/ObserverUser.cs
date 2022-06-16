using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi.src
{
    internal class ObserverUser
    {
        public IWebSocketConnection socket;
        public string userName;
        public ObserverUser(string userName, IWebSocketConnection socket)
        {
            this.userName = userName;
            this.socket = socket;
        }
        public void SetSocket(IWebSocketConnection socket)
        {
            this.socket = socket;
        }
        public bool notify(string msg)
        {
            if (socket.IsAvailable)
            {
                socket.Send(msg);
            }

            return true;
        }
    }
}
