using System.Threading.Tasks;

namespace MonitorEngine
{
    public interface IMonitor
    {
        Task RunAsync();
        bool StartOperation();
        void SearchDirectoriesAndDeleteIfNecessary();
        void SearchFilesAndDeleteIfNecessary(string directory);
    }
}