using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationA
{
    public class RecordModel
    {
        public int Id { get; set; }
        public String Value { get; set; }
        public ClientType Type { get; set; }
        public RecordStatus Status { get; set; }
    }
}
