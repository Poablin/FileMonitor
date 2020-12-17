using System.Collections.Generic;

namespace MonitorEngine.Utilities
{
    public interface IFileOperations
    {
        IEnumerable<string> GetDirectory(string path);
        IEnumerable<string> GetFiles(string directory);
        void DeleteFile(string file);
        void DeleteDirectory(string directory);
    }
}