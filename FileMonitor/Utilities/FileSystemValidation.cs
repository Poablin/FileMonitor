using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FileMonitor
{
    public class FileSystemValidation : IFileSystemValidation
    {
        private readonly string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmm");

        public bool CheckIfDirectoryIsEmpty(string directory)
        {
            return Directory.GetFiles(directory).Length == 0;
        }

        public bool CheckIfDirectoryIsCorrectFormat(string directory)
        {
            var directoryName = directory.Substring(directory.LastIndexOf('\\') + 1);
            return Regex.IsMatch(directoryName, @"[0-9]{8}");
        }

        public bool CheckIfFileIsCorrectFormat(string file)
        {
            var fileName = file.Substring(file.LastIndexOf('\\') + 1);
            return Regex.IsMatch(fileName, @"^[^.]{1,}.[^.]{1,}.\[IM-[\d]{7}]-\[[\d]{12}\]");
        }

        public bool CheckIfFileDateIsLessThanCurrentDate(string file)
        {
            var date = file.Substring(file.LastIndexOf('[')).Trim('[', ']');
            if (!DateTime.TryParseExact(date, "yyyyMMddHHmm", null, System.Globalization.DateTimeStyles.AssumeLocal, out _))
            {
                return false;
            }
            if (DateTime.ParseExact(date, "yyyyMMddHHmm", null) < DateTime.Now) return true;
            return false;
        }
    }
}