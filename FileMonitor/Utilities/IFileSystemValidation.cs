namespace FileMonitor
{
    public interface IFileSystemValidation
    {
        bool DirectoryIsCorrectFormat(string directory);
        bool FileIsCorrectFormat(string file);
        bool FileDateIsLessThanCurrentDate(string file);
    }
}