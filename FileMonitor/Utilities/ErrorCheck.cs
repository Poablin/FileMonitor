using System;
using System.IO;

namespace FileMonitor
{
    public class ErrorCheck : IErrorCheck
    {
        private readonly string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmm");

        public bool CheckIfPathExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool CheckIfDirectoryIsEmpty(string directory)
        {
            return Directory.GetFiles(directory).Length == 0;
        }

        public bool CheckIfDirectoryIsCorrectFormat(string directory)
        {
            var directoryName = directory.Substring(directory.LastIndexOf('\\') + 1);
            return directoryName.Length == 8 && long.TryParse(directoryName, out _);
        }

        public bool CheckIfFileIsCorrectFormat(string file)
        {
            if (file.LastIndexOf('[') == -1) return false;
            var fileDate = file.Substring(file.LastIndexOf('[')).Trim('[', ']');
            return fileDate.Length == 12 && long.TryParse(fileDate, out _);
        }

        public bool CheckIfFileDateIsLessThanCurrentDate(string file)
        {
            var date = DateTime.Parse(file.Substring(file.LastIndexOf('[')).Trim('[', ']'));
            if (date < DateTime.Now) return true;
            return false;
        }

        public bool IsValidFile()
        {
            string file = "";
            CheckIfFileIsCorrectFormat(file);
            CheckIfFileDateIsLessThanCurrentDate(file);
            return true;
        }
        public bool IsValidDirectory()
        {
            string directory = "";
            CheckIfDirectoryIsEmpty(directory);
            CheckIfDirectoryIsCorrectFormat(directory);
            return true;
        }
    }
}