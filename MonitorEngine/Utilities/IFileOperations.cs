namespace MonitorEngine.Utilities
{
    public interface IFileOperations
    {
        string[] GetDirectory(string path);
        string[] GetFiles(string directory);
        void DeleteFile(string file);
        void DeleteDirectory(string directory);
    }
}