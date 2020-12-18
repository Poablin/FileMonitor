using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FileMonitor
{
    public class FileSystemValidation : IFileSystemValidation
    {
        public bool DirectoryIsCorrectFormat(string directory)
        {
            var directoryName = directory.Substring(directory.LastIndexOf('\\') + 1);
            return Regex.IsMatch(directoryName, @"[0-9]{8}");
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

        public bool FileIsValid(string file)
        {
            if (FileIsCorrectFormat(file) && FileDateIsLessThanCurrentDate(file)) return true;
            else return false;
        }
    }
}