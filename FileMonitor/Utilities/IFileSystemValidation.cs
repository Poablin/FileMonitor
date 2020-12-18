namespace FileMonitor
{
    public interface IFileSystemValidation
    {
        bool CheckIfDirectoryIsEmpty(string directory);
        bool CheckIfDirectoryIsCorrectFormat(string directory);
        bool CheckIfFileIsCorrectFormat(string file);
        bool CheckIfFileDateIsLessThanCurrentDate(string file);
    }
}