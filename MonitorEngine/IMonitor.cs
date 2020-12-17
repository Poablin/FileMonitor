using System.Threading.Tasks;

namespace MonitorEngine
{
    public interface IMonitor
    {
        Task RunAsync();
        bool StartOperation();
        void SearchThroughDirectoriesAndDeleteIfNecessary();
        void SearchThroughFilesAndDeleteIfNecessary(string directory);
    }
}