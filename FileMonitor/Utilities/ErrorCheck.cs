using System;
using System.IO;
using System.Text.RegularExpressions;

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
            return Regex.IsMatch(directoryName, @"[0-9]{8}");
        }

        public bool CheckIfFileIsCorrectFormat(string file)
        {
            var fileName = file.Substring(file.LastIndexOf('\\') + 1);
            return Regex.IsMatch(fileName, @"^[^.]{1,}.[^.]{1,}.\[IM-[\d]{7}]-\[[\d]{12}\]");
        }

        public bool CheckIfFileDateIsLessThanCurrentDate(string file)
        {
            //var date = DateTime.Parse(file.Substring(file.LastIndexOf('[')).Trim('[', ']'));
            //if (date < DateTime.Now) return true;
            return true;
        }

        public bool CheckIfDoneFolderExists(string directory)
        {
            if (new DirectoryInfo(directory).Name == "Done")
            {
                return true;
            }
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