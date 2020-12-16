namespace FileMonitor
{
    interface IMonitor
    {
        void Run();

        bool SearchThroughFilesAndDeleteAsync();
    }
}
