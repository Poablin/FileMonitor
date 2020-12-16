using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var monitor = new Monitor();
            monitor.SearchThroughFiles();

            while(true)
            {
                var command = Console.ReadLine();
                if (command == "q") Environment.Exit(0);
            }
        }
    }
}
