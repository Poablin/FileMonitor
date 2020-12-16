using System;
using System.IO;
using System.Threading.Tasks;

namespace MonitorEngine
{
    public class Monitor
    {
        private readonly string Path = @"C:\Users\krist\Downloads\test\Done";

        public async Task Run()
        {
            await Task.FromResult(SearchThroughFilesAndDeleteAsync());
        }

        public bool SearchThroughFilesAndDeleteAsync()
        {
            var currentDateString = $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Second}";
            var currentDateInt = Convert.ToInt64(currentDateString);

            foreach (var directory in Directory.GetDirectories(Path))
            {
                var count = 0;
                if (directory.Substring(directory.LastIndexOf('\\') + 1).ToString().Length != 8) continue;
                foreach (var file in Directory.GetFiles(directory))
                {
                    try
                    {
                        if (Convert.ToInt64(file.Substring(file.LastIndexOf('[')).Trim('[', ']')) < currentDateInt)
                        {
                            if (count == 0) Console.WriteLine("Folder: " + directory);
                            count++;
                            File.Delete(file);
                            Console.WriteLine(file + " - Deleted");
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e);
                    }
                    if (Directory.GetFiles(directory).Length == 0) Directory.Delete(directory);
                }
            }

            return true;
        }
    }
}