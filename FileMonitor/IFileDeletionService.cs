namespace FileMonitor
{
    public interface IFileDeletionService
    {
        void Run();
        void SearchDirectoriesAndDeleteIfNecessary(string path);
        void SearchFilesAndDeleteIfNecessary(string directory);
    }
}