using System;
using System.IO;

namespace FileMonitor
{
    internal class Monitor
    {
        public Monitor()
        {

        }

        private string Path = @"C:\Users\krist\Downloads\test\Done";

        public void SearchThroughFiles()
        {
            var currentDateString = $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Second}";
            var currentDateInt = Convert.ToInt64(currentDateString);
            foreach (var directory in Directory.GetDirectories(Path))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    if (Convert.ToInt64(file.Substring(file.LastIndexOf('[')).Trim('[', ']')) < currentDateInt)
                    {
                        File.Delete(file);
                        Console.WriteLine(file + " - Deleted");
                    }
                }
                if (Directory.GetFiles(directory).Length == 0)
                {
                    Directory.Delete(directory);
                }
            }
        }
    }
}