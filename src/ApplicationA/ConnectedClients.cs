using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationA
{
    class ConnectedClients
    {
        private static List<ClientInfo> clients = null;
        private static readonly object objLock = new object();

        public static List<ClientInfo> Clients
        {
            get
            {
                lock (objLock)
                {
                    if (clients == null)
                    {
                        clients = new List<ClientInfo>();
                    }
                    return clients;
                }
            }
        }

    }
}
