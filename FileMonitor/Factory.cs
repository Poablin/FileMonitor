using MonitorEngine;
using MonitorEngine.Utilities;

namespace FileMonitor
{
    public static class Factory
    {
        private static ILogger CreateLogger()
        {
            return new Logger();
        }

        private static IErrorCheck CreateErrorCheck()
        {
            return new ErrorCheck();
        }

        public static IMonitor CreateMonitor()
        {
            return new Monitor(CreateLogger(), CreateErrorCheck());
        }
    }
}