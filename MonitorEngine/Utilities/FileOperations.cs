using System.Collections.Generic;
using System.IO;

namespace MonitorEngine.Utilities
{
    public class FileOperations : IFileOperations
    {
        public IEnumerable<string> GetDirectory(string path)
        {
            return Directory.GetDirectories(path);
        }

        public IEnumerable<string> GetFiles(string directory)
        {
            return Directory.GetFiles(directory);
        }

        public void DeleteFile(string file)
        {
            File.Delete(file);
        }

        public void DeleteDirectory(string directory)
        {
            Directory.Delete(directory);
        }
    }
}