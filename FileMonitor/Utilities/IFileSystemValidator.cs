using System.IO;

namespace FileMonitor.Utilities
{
    public interface IFileSystemValidator
    {
        bool DirectoryIsCorrectFormat(string directory);

        bool DirectoryIsADate(string directory);

        bool FileIsCorrectFormat(string file);

        bool FileDateIsLessThanCurrentDate(string file);

        bool TryGetDoneFolder(string path, out DirectoryInfo doneFolder);

        bool DirectoryIsValid(string directory);

        bool FileIsValid(string file);
    }
}