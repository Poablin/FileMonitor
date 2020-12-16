using System.Threading.Tasks;

namespace MonitorEngine
{
    public interface IMonitor
    {
        Task Run();
        bool SearchThroughFilesAndDeleteAsync();
    }
}