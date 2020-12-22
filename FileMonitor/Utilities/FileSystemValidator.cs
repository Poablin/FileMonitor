using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace FileMonitor.Utilities
{
    public class FileSystemValidator : IFileSystemValidator
    {
        private static readonly Regex ValidFileRegex =
            new Regex(@"^.*[.]\[IM-\d+]-\[(?<deleteDate>\d{12})]$", RegexOptions.Compiled);

        private static readonly Regex ValidDirectoryRegex =
            new Regex(@"^[\d]{4}[0-1][\d][0-3][\d]$", RegexOptions.Compiled);

        public bool TryGetDoneFolder(string path, out DirectoryInfo doneFolder)
        {
            doneFolder = new DirectoryInfo(Path.Combine(path, "Done"));
            return doneFolder.Exists;
        }

        public bool DirectoryIsValid(string directoryName)
        {
            var isCorrectFormat = ValidDirectoryRegex.IsMatch(directoryName);
            var isValidDate = DateTime.TryParseExact(directoryName, "yyyyMMdd", null, DateTimeStyles.AssumeLocal,
                out _);
            return isCorrectFormat && isValidDate;
        }

        public bool FileIsValid(string fileName)
        {
            var match = ValidFileRegex.Match(fileName);
            var isValidDate = DateTime.TryParseExact(match.Groups["deleteDate"].Value, "yyyyMMddHHmm",
                null, DateTimeStyles.AssumeLocal,
                out var validDate);

            return match.Success && isValidDate && validDate < DateTime.Now;
        }
    }
}