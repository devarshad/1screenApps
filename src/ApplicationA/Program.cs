using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationA
{
    class Program
    {

        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {

                using (var host = new ServiceHost(typeof(JobDitributer)))
                {
                    host.Open();
                    Console.WriteLine("Application A Started...");
                    QuatumIdentifier.Start("App_Data/InputData.xml");
                    Console.Read();
                }
                mutex.ReleaseMutex();
            }
        }
    }
}
