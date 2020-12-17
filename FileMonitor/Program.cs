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
                var run = monitor.Run();
                await run;
                Thread.Sleep(5000);
            }
        }
    }
}