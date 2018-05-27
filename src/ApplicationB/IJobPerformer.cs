using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IJobPerformer" in both code and config file together.
    [ServiceContract]
    public interface IJobPerformer
    {
        [OperationContract]
        Boolean IsLive();

        [OperationContract]
        Task<Boolean> DoWork(String value);
    }
}
