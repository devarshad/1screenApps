using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "JobPerformer" in both code and config file together.
    public class JobPerformer : IJobPerformer
    {
        public Boolean IsLive()
        {
            return true;
        }

        public async Task<Boolean> DoWork(String value)
        {
            return await Processor.Process(value);
        }
    }
}
