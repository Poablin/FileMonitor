using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace FileMonitor.Utilities
{
    public class FileSystemValidator : IFileSystemValidator
    {
        public bool TryGetDoneFolder(string path, out DirectoryInfo doneFolder)
        {
            doneFolder = new DirectoryInfo(Path.Combine(path, "Done"));
            return doneFolder.Exists;
        }

        public bool DirectoryIsValid(string directoryName)
        {
            var isCorrectFormat = Regex.IsMatch(directoryName, @"^[\d]{4}[0-1][\d][0-3][\d]$");
            var isValidDate = DateTime.TryParseExact(directoryName, "yyyyMMdd", null, DateTimeStyles.AssumeLocal,
                out _);
            return isCorrectFormat && isValidDate;
        }

        public bool FileIsValid(string fileName)
        {
            var isCorrectFormat = Regex.IsMatch(fileName, @"^.*[.]\[IM-\d+]-\[(?<deleteDate>\d{12})]$");
            if (!isCorrectFormat) return false;
            var fileDate = fileName.Substring(fileName.LastIndexOf('[')).Trim('[', ']');
            var isValidDate = DateTime.TryParseExact(fileDate, "yyyyMMddHHmm", null, DateTimeStyles.AssumeLocal,
                out var validDate);
            return isValidDate && validDate < DateTime.Now;
        }
    }
}