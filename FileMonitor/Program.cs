using System.Threading;

namespace FileMonitor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var monitor = new MonitorEngine.Monitor();

            while (true)
            {
                monitor.Run();
                Thread.Sleep(900000);
            }
        }
    }
}