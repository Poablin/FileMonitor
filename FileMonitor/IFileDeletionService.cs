using System.IO;

namespace FileMonitor
{
    public interface IFileDeletionService
    {
        void Run();

        void DeleteFolderIfEmpty(DirectoryInfo subFolder);
    }
}