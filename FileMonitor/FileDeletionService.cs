using System.IO;
using System.Linq;
using FileMonitor.Utilities;

namespace FileMonitor
{
    public class FileDeletionService : IFileDeletionService
    {
        private readonly IFileSystemValidator _fileSystemValidator;
        private readonly ILogger _logger;
        private readonly string[] _paths = { @"Enter paths here" };

        public FileDeletionService(ILogger logger, IFileSystemValidator fileSystemValidation)
        {
            _logger = logger;
            _fileSystemValidator = fileSystemValidation;
        }

        public void Run()
        {
            try
            {
                foreach (var path in _paths)
                {
                    if (!_fileSystemValidator.TryGetDoneFolder(path, out var doneFolder))
                    {
                        _logger.Log("Done folder does not exist");
                        continue;
                    }

                    var subFolders = doneFolder.EnumerateDirectories()
                        .Where(x => _fileSystemValidator.DirectoryIsValid(x.Name));

                    foreach (var subFolder in subFolders)
                    {
                        var filesToDelete = subFolder.EnumerateFiles()
                            .Where(x => _fileSystemValidator.FileIsValid(x.Name));

                        foreach (var fileToDelete in filesToDelete)
                        {
                            _logger.Log("File: " + fileToDelete.Name + " - Deleted");
                            fileToDelete.Delete();
                        }

                        DeleteFolderIfEmpty(subFolder);
                    }
                }
            }
            catch (IOException e)
            {
                _logger.Log(e.ToString());
            }
        }

        public void DeleteFolderIfEmpty(DirectoryInfo subFolder)
        {
            try
            {
                if (!subFolder.EnumerateFiles().Any())
                {
                    _logger.Log("Folder at " + subFolder.FullName + " - Deleted");
                    subFolder.Delete();
                }
            }
            catch (IOException e)
            {
                _logger.Log(e.ToString());
            }
        }
    }
}