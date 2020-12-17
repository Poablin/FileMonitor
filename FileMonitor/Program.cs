using System.Threading;
using System.Threading.Tasks;

namespace FileMonitor
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var monitor = Factory.CreateMonitor();
            while (true)
            {
                await monitor.RunAsync();
                Thread.Sleep(900000);
            }
        }
    }
}