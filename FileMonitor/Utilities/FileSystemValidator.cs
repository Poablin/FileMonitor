using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FileMonitor
{
    public class FileSystemValidator : IFileSystemValidator
    {
        public bool DirectoryIsCorrectFormat(string directory)
        {
            var directoryName = directory.Substring(directory.LastIndexOf('\\') + 1);
            return Regex.IsMatch(directoryName, @"^[0-9]{4}[0-1][0-9][0-3][0-9]");
        }

        public bool DirectoryIsADate(string directory)
        {
            var directoryName = directory.Substring(directory.LastIndexOf('\\') + 1);
            return DateTime.TryParseExact(directoryName, "yyyyMMdd", null, DateTimeStyles.AssumeLocal, out _);
        }

        public bool FileIsCorrectFormat(string file)
        {
            var fileName = file.Substring(file.LastIndexOf('\\') + 1);
            return Regex.IsMatch(fileName, @"^[^.]{1,}.[^.]{1,}.\[IM-[\d]{7}]-\[[\d]{12}\]");
        }

        public bool FileDateIsLessThanCurrentDate(string file)
        {
            var date = file.Substring(file.LastIndexOf('[')).Trim('[', ']');
            var success = DateTime.TryParseExact(date, "yyyyMMddHHmm", null, DateTimeStyles.AssumeLocal, out _);
            if (success) return DateTime.ParseExact(date, "yyyyMMddHHmm", null) < DateTime.Now;
            else return false;
        }

        public bool DirectoryIsValid(string directory)
        {
            if (DirectoryIsCorrectFormat(directory) && DirectoryIsADate(directory)) return true;
            else return false;
        }

        public bool FileIsValid(string file)
        {
            if (FileIsCorrectFormat(file) && FileDateIsLessThanCurrentDate(file)) return true;
            else return false;
        }
    }
}