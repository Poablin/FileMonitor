namespace MonitorEngine.Utilities
{
    public interface IErrorCheck
    {
        bool CheckIfPathExists(string path);
        bool CheckIfDirectoryIsEmpty(string directory);
        bool CheckIfDirectoryIsCorrectFormat(string directory);
        bool CheckIfFileIsCorrectFormat(string file);
        bool CheckIfFileDateIsLessThanCurrentDate(string file);
    }
}