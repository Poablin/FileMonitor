namespace MonitorEngine
{
    interface IMonitor
    {
        void Run();

        bool SearchThroughFilesAndDeleteAsync();
    }
}
