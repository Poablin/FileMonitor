using MonitorEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitor
{
    public static class Factory
    {
        public static ILogger CreateLogger()
        {
            return new Logger();
        }

        public static IErrorCheck CreateErrorCheck()
        {
            return new ErrorCheck();
        }
    }
}
