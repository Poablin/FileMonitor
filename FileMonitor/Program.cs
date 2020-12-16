using System.Threading;
using System.Threading.Tasks;

namespace FileMonitor
{
    internal class Program
    {
        static async Task Main(string[] args)
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