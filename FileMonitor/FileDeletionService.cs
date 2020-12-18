using System.IO;

namespace FileMonitor
{
    public class FileDeletionService : IFileDeletionService
    {
        private readonly string[] _paths = { @"C:\Users\krist\Downloads\input" }; //eks C:\Users\test\Downloads
        private readonly IErrorCheck _errorCheck;
        private readonly ILogger _logger;
        private int _fileCount;

        public FileDeletionService(ILogger logger, IErrorCheck errorCheck)
        {
            _logger = logger;
            _errorCheck = errorCheck;
        }

        public void Run()
        {
            foreach (var path in _paths)
            {
                if (!_errorCheck.CheckIfPathExists(path))
                {
                    _logger.Log("Path does not exist");
                    return;
                }
                foreach (var directory in Directory.GetDirectories(path))
                {
                    if (_errorCheck.CheckIfDoneFolderExists(directory))
                    {
                        SearchDirectoriesAndDeleteIfNecessary(directory);
                    }
                }
            }
        }

        public void SearchDirectoriesAndDeleteIfNecessary(string path)
        {
            foreach (var directory in Directory.GetDirectories(path))
            {
                _fileCount = 0;
                if (!_errorCheck.CheckIfDirectoryIsCorrectFormat(directory)) continue;
                SearchFilesAndDeleteIfNecessary(directory);
                if (!_errorCheck.CheckIfDirectoryIsEmpty(directory)) continue;
                Directory.Delete(directory);
                _logger.Log("Folder: " + directory + " - Deleted");
            }
        }

        public void SearchFilesAndDeleteIfNecessary(string directory)
        {
            try
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    if (!_errorCheck.CheckIfFileIsCorrectFormat(file)) continue;
                    if (!_errorCheck.CheckIfFileDateIsLessThanCurrentDate(file)) continue;
                    if (_fileCount == 0) _logger.Log("Folder: " + directory);
                    _fileCount++;
                    File.Delete(file);
                    _logger.Log(file + " - Deleted");
                }
            }
            catch (IOException e)
            {
                _logger.Log(e.ToString());
            }
        }
    }
}