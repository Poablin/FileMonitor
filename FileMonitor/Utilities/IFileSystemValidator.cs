using System.IO;

namespace FileMonitor.Utilities
{
    public interface IFileSystemValidator
    {
        bool TryGetDoneFolder(string path, out DirectoryInfo doneFolder);

        bool DirectoryIsValid(string directoryName);

        bool FileIsValid(string fileName);
    }
}