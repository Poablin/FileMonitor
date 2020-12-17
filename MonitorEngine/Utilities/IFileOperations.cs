using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
