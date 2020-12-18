using System.IO;

namespace FileMonitor
{
    public class FileDeletionService : IFileDeletionService
    {
        private readonly string[] _paths = { @"C:\Users\krist\Downloads\input" }; //eks C:\Users\test\Downloads
        private readonly IFileSystemValidation _fileSystemValidation;
        private readonly ILogger _logger;
        private int _fileCount;

        public FileDeletionService(ILogger logger, IFileSystemValidation fileSystemValidation)
        {
            _logger = logger;
            _fileSystemValidation = fileSystemValidation;
        }

        public void Run()
        {
            foreach (var path in _paths)
            {
                if (Directory.Exists(path) == false)
                {
                    _logger.Log("Path does not exist");
                    return;
                }
                foreach (var directory in Directory.GetDirectories(path))
                {
                    if (new DirectoryInfo(directory).Name == "Done")
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
                if (_fileSystemValidation.CheckIfDirectoryIsCorrectFormat(directory))
                {
                    SearchFilesAndDeleteIfNecessary(directory);
                    if (_fileSystemValidation.CheckIfDirectoryIsEmpty(directory))
                    {
                        Directory.Delete(directory);
                        _logger.Log("Folder: " + directory + " - Deleted");
                    };
                };
            }
        }

        public void SearchFilesAndDeleteIfNecessary(string directory)
        {
            try
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    if (_fileSystemValidation.CheckIfFileIsCorrectFormat(file))
                    {
                        if (_fileSystemValidation.CheckIfFileDateIsLessThanCurrentDate(file))
                        {
                            if (_fileCount == 0) _logger.Log("Folder: " + directory);
                            _fileCount++;
                            File.Delete(file);
                            _logger.Log(file + " - Deleted");
                        }
                    }
                }
            }
            catch (IOException e)
            {
                _logger.Log(e.ToString());
            }
        }
    }
}