using System.IO;

namespace FileMonitor.Utilities
{
    public interface IFileSystemValidator
    {
        bool DirectoryIsCorrectFormat(string directoryName);

        bool DirectoryIsADate(string directoryName);

        bool FileIsCorrectFormat(string fileName);

        bool FileDateIsLessThanCurrentDate(string fileName);

        bool TryGetDoneFolder(string path, out DirectoryInfo doneFolder);

        bool DirectoryIsValid(string directoryName);

        bool FileIsValid(string fileName);
    }
}