using System.Threading;
using System.Threading.Tasks;
using FileMonitor.Utilities;

namespace FileMonitor
{
    internal static class Program
    {
        private static Task Main(string[] args)
        {
            var fileDeletionService = new FileDeletionService(new Logger(), new FileSystemValidator());
            while (true)
            {
                fileDeletionService.Run();
                Thread.Sleep(900000);
            }
        }
    }
}