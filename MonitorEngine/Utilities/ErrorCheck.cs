using System;
using System.Text.RegularExpressions;

namespace MonitorEngine.Utilities
{
    public class ErrorCheck : IErrorCheck
    {
        public bool CheckIfDirectoryIsCorrectFormat(string directory)
        {
            var directoryName = directory.Substring(directory.LastIndexOf('\\') + 1).ToString();
            if (directoryName.Length != 8)
            {
                if (Regex.IsMatch(directoryName, @"[0001-9999][01-12][01-32]"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
