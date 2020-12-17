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

        private static IFileOperations CreateFileOperations()
        {
            return new FileOperations();
        }

        public static IMonitor CreateMonitor()
        {
            return new Monitor(CreateLogger(), CreateErrorCheck(), CreateFileOperations());
        }
    }
}