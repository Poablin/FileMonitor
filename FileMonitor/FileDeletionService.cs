using System.IO;
using FileMonitor.Utilities;

namespace FileMonitor
{
    public class FileDeletionService : IFileDeletionService
    {
        private readonly IFileSystemValidator _fileSystemValidator;
        private readonly ILogger _logger;
        private string[] _paths = { @"Enter paths here" };
        private int _fileCount;

        public FileDeletionService(ILogger logger, IFileSystemValidator fileSystemValidation)
        {
            _logger = logger;
            _fileSystemValidator = fileSystemValidation;
        }

        public void Run()
        {
            foreach (var path in _paths)
            {
                if (!Directory.Exists(path))
                {
                    _logger.Log("Path does not exist");
                    return;
                }

                foreach (var directory in Directory.GetDirectories(path))
                    if (new DirectoryInfo(directory).Name == "Done")
                        SearchDirectoriesAndDeleteIfNecessary(directory);
            }
        }

        public void SearchDirectoriesAndDeleteIfNecessary(string path)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(path))
                {
                    _fileCount = 0;
                    if (!_fileSystemValidator.DirectoryIsValid(directory)) continue;
                    SearchFilesAndDeleteIfNecessary(directory);
                    if (Directory.GetFiles(directory).Length == 0)
                    {
                        Directory.Delete(directory);
                        _logger.Log("Folder: " + directory + " - Deleted");
                    }
                }
            }
            catch (IOException e)
            {
                _logger.Log(e.ToString());
            }
        }

        public void SearchFilesAndDeleteIfNecessary(string directory)
        {
            try
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    if (!_fileSystemValidator.FileIsValid(file)) continue;
                    if (_fileCount == 0) _logger.Log("Folder: " + directory);
                    _fileCount++;
                    File.Delete(file);
                    _logger.Log("File: " + new FileInfo(file).Name + " - Deleted");
                }
            }
            catch (IOException e)
            {
                _logger.Log(e.ToString());
            }
        }
    }
}