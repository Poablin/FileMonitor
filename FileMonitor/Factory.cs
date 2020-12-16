using MonitorEngine;
using MonitorEngine.Utilities;

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
        public static IMonitor CreateMonitor()
        {
            return new Monitor(CreateLogger(), CreateErrorCheck());
        }
    }
}
