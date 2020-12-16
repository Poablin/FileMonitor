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
        public static Logger CreateLogger()
        {
            return new Logger();
        }

        public static ErrorCheck CreateErrorCheck()
        {
            return new ErrorCheck();
        }
    }
}
