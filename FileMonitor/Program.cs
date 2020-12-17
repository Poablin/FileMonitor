using System.Threading;
using System.Threading.Tasks;
using MonitorEngine.Utilities;

namespace FileMonitor
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var monitor = new MonitorEngine.Monitor(new Logger(), new ErrorCheck(), new FileOperations());
            while (true)
            {
                await monitor.RunAsync();
                Thread.Sleep(900000);
            }
        }
    }
}