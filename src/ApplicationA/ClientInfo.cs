using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationA
{
    class ClientInfo
    {
        public Guid ID { get; set; }
        public ClientService.JobPerformerClient Client { get; set; }
        public Boolean Status { get; set; }
        public ClientType Type { get; set; }
        public int AssignedTask { get; set; }
    }
}
