using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length==0)
            {
                Console.WriteLine("Please specify the type of Worker(Application B)...");
                Console.ReadKey();
                return;
            }
            using (var host = new ServiceHost(typeof(JobPerformer)))
            {
                try
                {
                    host.Open();
                }
                catch (AddressAlreadyInUseException)
                {

                }           
                Console.WriteLine("Application B:" + args[0] + " Started...");

                var _server = new ServerService.JobDitributerClient();
                try
                {
                    _server.Connect(args[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
            }
        }
    }
}
