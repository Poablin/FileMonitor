namespace FileMonitor
{
    public interface IFileSystemValidation
    {
        bool DirectoryIsEmpty(string directory);
        bool DirectoryIsCorrectFormat(string directory);
        bool FileIsCorrectFormat(string file);
        bool FileDateIsLessThanCurrentDate(string file);
    }
}