using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationA
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "JobDitributer" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class JobDitributer : IJobDitributer
    {
        public Boolean Connect(String Type)
        {
            try
            {
                var _client = new ClientService.JobPerformerClient();
                ConnectedClients.Clients.Add(new ClientInfo
                {
                    ID = new Guid(),
                    Client = _client,
                    Status = true,
                    Type = Type.ToEnum<ClientType>(),
                    AssignedTask = 0
                });
                Console.WriteLine("New worker of type: \"" + Type + "\" connected.");
            }
            catch (ArgumentException)
            {
                throw new FaultException("Type: \"" + Type + "\" is not defined.");
            }
            return true;
        }

        public async Task<Boolean> AssignAsync(RecordModel model)
        {
            var _client = FindBestClient(model.Type);

            if (_client == null)
            {
                return false;
            }
            try
            {
                Console.WriteLine(String.Format("Processing: Type: {0}, Value: {1}", model.Type, model.Value));
                _client.AssignedTask++;
                if (await _client.Client.DoWorkAsync(model.Value))
                {
                    _client.AssignedTask--;
                    return true;
                }
            }
            catch (Exception)
            {
                _client.AssignedTask--;
            }
            return false;
        }

        internal ClientInfo FindBestClient(ClientType type)
        {
            var _this_type_clients = ConnectedClients.Clients.Where(x => x.Type == type).OrderBy(x => x.AssignedTask);

            foreach (var client in _this_type_clients)
            {
                try
                {
                    if (client.Client.IsLive())
                    {
                        return client;
                    }
                }
                catch (Exception)
                {
                    Disconnect(client);
                }
            }
            return null;
        }

        internal void Disconnect(ClientInfo client)
        {
            ConnectedClients.Clients.Remove(client);
        }
    }
}
