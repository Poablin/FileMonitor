using FileMonitor.Utilities;
using System.IO;
using System.Linq;

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
                        try
                        {
                            fileToDelete.Delete();
                            _logger.Log($"File: {fileToDelete.Name} - Deleted");
                        }
                        catch (IOException e)
                        {
                            _logger.Log(e.Message);
                        }

                    DeleteFolderIfEmpty(subFolder);
                }
            }
        }

        public void DeleteFolderIfEmpty(DirectoryInfo subFolder)
        {
            if (subFolder.EnumerateFiles().Any()) return;
            try
            {
                subFolder.Delete();
                _logger.Log($"Folder at {subFolder.FullName} - Deleted");
            }
            catch (IOException e)
            {
                _logger.Log(e.Message);
            }
        }
    }
}