using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MonitorEngine.Utilities
{
    public class ErrorCheck : IErrorCheck
    {
        public bool CheckIfDirectoryIsEmpty(string directory)
        {
            if (Directory.GetFiles(directory).Length == 0) return true;
            return false;
        }

        public bool CheckIfDirectoryIsCorrectFormat(string directory)
        {
            var directoryName = directory.Substring(directory.LastIndexOf('\\') + 1);
            if (directoryName.Length != 8)
            {
                if (Regex.IsMatch(directoryName, @"[0001-9999][01-12][01-32]"))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckIfFileIsCorrectFormat(string file)
        {
            //var fileId = file.Substring(file.LastIndexOf('M') + 2);
            //if (!Regex.IsMatch(fileId, @"[0000000-9999999]")) return false;
            var fileDate = file.Substring(file.LastIndexOf('[')).Trim('[', ']');
            if (fileDate.Length != 12) return false;
            if (Regex.IsMatch(fileDate, @"[0001-9999][01-12][01-32]"))
            {
                return true;
            }
            return false;
        }
        public bool CheckIfFileDateIsLessThanCurrentDate(string file, long currentDate)
        {
            if (Convert.ToInt64(file.Substring(file.LastIndexOf('[')).Trim('[', ']')) < currentDate)
            {
                return true;
            }
            return false;
        }
    }
}
