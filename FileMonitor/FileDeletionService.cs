using System;
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

                    var validSubDirectories = doneFolder.EnumerateDirectories()
                        .Where(x => _fileSystemValidator.DirectoryIsValid(x.Name));

                    foreach (var subFolder in validSubDirectories)
                    {
                        var filesToDelete = subFolder.EnumerateFiles()
                            .Where(x => _fileSystemValidator.FileIsValid(x.Name));

                        foreach (var fileToDelete in filesToDelete)
                        {
                            fileToDelete.Delete();
                            _logger.Log($"File: {fileToDelete.Name} - Deleted");
                        }

                        DeleteFolderIfEmpty(subFolder);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
        }

        private void DeleteFolderIfEmpty(DirectoryInfo subFolder)
        {
            if (subFolder.EnumerateFiles().Any()) return;
            subFolder.Delete();
            _logger.Log($"Folder at {subFolder.FullName} - Deleted");
        }
    }
}