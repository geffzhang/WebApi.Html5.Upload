using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUploadFile
{
    class Program
    {
        static void Main()
        {
            UploadTest test = new UploadTest();
            test.Test();
            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}
