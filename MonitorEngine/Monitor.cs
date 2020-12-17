using System.IO;
using System.Threading.Tasks;
using MonitorEngine.Utilities;

namespace MonitorEngine
{
    public class Monitor : IMonitor
    {
        private const string Path = @"";
        private readonly IErrorCheck _errorCheck;
        private readonly IFileOperations _fileOperations;
        private readonly ILogger _logger;
        private int _count;

        public Monitor(ILogger logger, IErrorCheck errorCheck, IFileOperations fileOperations)
        {
            _logger = logger;
            _errorCheck = errorCheck;
            _fileOperations = fileOperations;
        }

        public async Task RunAsync()
        {
            if (!_errorCheck.CheckIfPathExists(Path))
            {
                _logger.Log("Path does not exist");
                return;
            }
            await Task.FromResult(StartOperation());
        }

        public bool StartOperation()
        {
            try
            {
                SearchThroughDirectoriesAndDeleteIfNecessary();
            }
            catch (IOException e)
            {
                _logger.Log(e.ToString());
            }

            return true;
        }

        public void SearchThroughDirectoriesAndDeleteIfNecessary()
        {
            foreach (var directory in _fileOperations.GetDirectory(Path))
            {
                _count = 0;
                if (_errorCheck.CheckIfDirectoryIsCorrectFormat(directory)) continue;
                SearchThroughFilesAndDeleteIfNecessary(directory);
                if (_errorCheck.CheckIfDirectoryIsEmpty(directory)) _fileOperations.DeleteDirectory(directory);
            }
        }

        public void SearchThroughFilesAndDeleteIfNecessary(string directory)
        {
            foreach (var file in _fileOperations.GetFiles(directory))
            {
                if (!_errorCheck.CheckIfFileIsCorrectFormat(file)) continue;
                if (!_errorCheck.CheckIfFileDateIsLessThanCurrentDate(file)) continue;
                if (_count == 0) _logger.Log("Folder: " + directory);
                _count++;
                _fileOperations.DeleteFile(file);
                _logger.Log(file + " - Deleted");
            }
            if (_count > 0) _logger.Log("");
        }
    }
}