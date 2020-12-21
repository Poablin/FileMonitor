using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace FileMonitor.Utilities
{
    public class FileSystemValidator : IFileSystemValidator
    {
        public bool DirectoryIsCorrectFormat(string directoryName)
        {
            return Regex.IsMatch(directoryName, @"^[0-9]{4}[0-1][0-9][0-3][0-9]");
        }

        public bool DirectoryIsADate(string directoryName)
        {
            return DateTime.TryParseExact(directoryName, "yyyyMMdd", null, DateTimeStyles.AssumeLocal, out _);
        }

        public bool FileIsCorrectFormat(string fileName)
        {
            return Regex.IsMatch(fileName, @"^.*[.]\[IM-\d+]-\[(?<deleteDate>\d{12})]$");
        }

        public bool FileDateIsLessThanCurrentDate(string fileName)
        {
            var fileDate = fileName.Substring(fileName.LastIndexOf('[')).Trim('[', ']');
            var success = DateTime.TryParseExact(fileDate, "yyyyMMddHHmm", null, DateTimeStyles.AssumeLocal, out var result);
            return success && result < DateTime.Now;
        }

        public bool TryGetDoneFolder(string path, out DirectoryInfo doneFolder)
        {
            doneFolder = new DirectoryInfo(Path.Combine(path, "Done"));
            return doneFolder.Exists;
        }

        public bool DirectoryIsValid(string directoryName)
        {
            return DirectoryIsCorrectFormat(directoryName) && DirectoryIsADate(directoryName);
        }

        public bool FileIsValid(string fileName)
        {
            return FileIsCorrectFormat(fileName) && FileDateIsLessThanCurrentDate(fileName);
        }
    }
}