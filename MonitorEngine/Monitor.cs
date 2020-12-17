﻿using System;
using System.IO;
using System.Threading.Tasks;
using MonitorEngine.Utilities;

namespace MonitorEngine
{
    public class Monitor : IMonitor
    {
        private const string Path = @"C:\Users\krist\Downloads\test\Done";
        private readonly IErrorCheck _errorCheck;
        private readonly ILogger _logger;

        public Monitor(ILogger logger, IErrorCheck errorCheck)
        {
            _logger = logger;
            _errorCheck = errorCheck;
        }

        public async Task Run()
        {
            await Task.FromResult(SearchThroughFilesAndDeleteAsync());
        }

        public bool SearchThroughFilesAndDeleteAsync()
        {
            var currentDateString = DateTime.Now.ToString("yyyyMMddHHmm");
            var currentDateLong = Convert.ToInt64(currentDateString);
            _logger.Log("currentDateString: " + currentDateString);

            foreach (var directory in Directory.GetDirectories(Path))
            {
                var count = 0;
                if (_errorCheck.CheckIfDirectoryIsCorrectFormat(directory)) continue;
                foreach (var file in Directory.GetFiles(directory))
                {
                    try
                    {
                        if (_errorCheck.CheckIfFileIsCorrectFormat(file) == false) continue;
                        if (_errorCheck.CheckIfFileDateIsLessThanCurrentDate(file, currentDateLong))
                        {
                            if (count == 0) _logger.Log("Folder: " + directory);
                            count++;
                            File.Delete(file);
                            _logger.Log(file + " - Deleted");
                        }
                    }
                    catch (IOException e)
                    {
                        _logger.Log(e.ToString());
                    }

                    if (_errorCheck.CheckIfDirectoryIsEmpty(directory)) Directory.Delete(directory);
                }
            }

            return true;
        }
    }
}