using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var monitor = new Monitor();

            while(true)
            {
                monitor.SearchThroughFiles();
                Thread.Sleep(900000);
            }
        }
    }
}
