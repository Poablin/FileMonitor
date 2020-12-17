using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MonitorEngine.Utilities
{
    public class ErrorCheck : IErrorCheck
    {
        public bool CheckIfDirectoryIsEmpty(string directory)
        {
            return Directory.GetFiles(directory).Length == 0;
        }

        public bool CheckIfDirectoryIsCorrectFormat(string directory)
        {
            var directoryName = directory.Substring(directory.LastIndexOf('\\') + 1);
            return directoryName.Length != 8 && Regex.IsMatch(directoryName, @"[0001-9999][01-12][01-32]");
        }

        public bool CheckIfFileIsCorrectFormat(string file)
        {
            var fileDate = file.Substring(file.LastIndexOf('[')).Trim('[', ']');
            return fileDate.Length == 12 && Regex.IsMatch(fileDate, @"[0001-9999][01-12][01-32]");
        }

        public bool CheckIfFileDateIsLessThanCurrentDate(string file, long currentDate)
        {
            return Convert.ToInt64(file.Substring(file.LastIndexOf('[')).Trim('[', ']')) < currentDate;
        }
    }
}