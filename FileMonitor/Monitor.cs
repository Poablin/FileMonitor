using System;
using System.IO;

namespace FileMonitor
{
    internal class Monitor
    {
        public Monitor(string path)
        {
            Path = path;
        }
        private string Path;

        public void SearchThroughFiles()
        {
            var files = Directory.GetFiles(Path);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }
    }
}