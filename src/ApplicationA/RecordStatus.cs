using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationA
{
    public enum RecordStatus : byte
    {
        Pending,
        Processing,
        Processed
    }
}
