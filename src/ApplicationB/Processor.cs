using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB
{
    class Processor
    {
        internal static async Task<Boolean> Process(string value)
        {
            Console.WriteLine(value);
            return await Task.FromResult<Boolean>(true);
        }
    }
}
