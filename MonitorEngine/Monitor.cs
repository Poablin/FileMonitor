using MonitorEngine.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MonitorEngine
{
    public class Monitor : IMonitor
    {
        public Monitor(ILogger logger, IErrorCheck errorCheck)
        {
            _logger = logger;
            _errorCheck = errorCheck;
        }

        private ILogger _logger;
        private IErrorCheck _errorCheck;
        private const string Path = @"C:\Users\krist\Downloads\test\Done";

        public async Task Run()
        {
            await Task.FromResult(SearchThroughFilesAndDeleteAsync());
        }

        public bool SearchThroughFilesAndDeleteAsync()
        {
            var currentDateString = DateTime.Now.ToString("yyyyMMddHHmm");
            var currentDateInt = Convert.ToInt64(currentDateString);
            _logger.Log("currentDateString: " + currentDateString);

            foreach (var directory in Directory.GetDirectories(Path))
            {
                var count = 0;
                if (_errorCheck.CheckIfDirectoryIsCorrectFormat(directory)) continue;
                foreach (var file in Directory.GetFiles(directory))
                {
                    try
                    {
                        if (Convert.ToInt64(file.Substring(file.LastIndexOf('[')).Trim('[', ']')) < currentDateInt)
                        {
                            if (count == 0) Console.WriteLine("Folder: " + directory);
                            count++;
                            File.Delete(file);
                            _logger.Log(file + " - Deleted");
                        }
                    }
                    catch (IOException e)
                    {
                        _logger.Log(e.ToString());
                    }
                    if (Directory.GetFiles(directory).Length == 0) Directory.Delete(directory);
                }
            }

            return true;
        }
    }
}