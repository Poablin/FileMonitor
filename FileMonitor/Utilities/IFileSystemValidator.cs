namespace FileMonitor.Utilities
{
    public interface IFileSystemValidator
    {
        bool DirectoryIsCorrectFormat(string directory);

        bool DirectoryIsADate(string directory);

        bool FileIsCorrectFormat(string file);

        bool FileDateIsLessThanCurrentDate(string file);

        bool DirectoryIsValid(string directory);

        bool FileIsValid(string file);
    }
}