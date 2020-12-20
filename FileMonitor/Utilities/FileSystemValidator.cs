using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FileMonitor.Utilities
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
            var fileDate = file.Substring(file.LastIndexOf('[')).Trim('[', ']');
            var success = DateTime.TryParseExact(fileDate, "yyyyMMddHHmm", null, DateTimeStyles.AssumeLocal, out _);
            return success && DateTime.ParseExact(fileDate, "yyyyMMddHHmm", null) < DateTime.Now;
        }

        public bool DirectoryIsValid(string directory)
        {
            return DirectoryIsCorrectFormat(directory) && DirectoryIsADate(directory);
        }

        public bool FileIsValid(string file)
        {
            return FileIsCorrectFormat(file) && FileDateIsLessThanCurrentDate(file);
        }
    }
}