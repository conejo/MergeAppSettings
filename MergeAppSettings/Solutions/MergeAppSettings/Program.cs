using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeAppSettings
{
    using System.IO;
    using System.Security.Cryptography;

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please enter shared and target config file names.");
                Console.ReadKey();
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Can't find: {0}", args[0]);
                Console.ReadKey();
                return;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine("Can't find: {0}", args[1]);
                Console.ReadKey();
                return;
            }

            

            var processor = new AppSettingsProcessor(args[0], args[1]);
            processor.Process();

            Console.ReadKey();
        }
    }
}
