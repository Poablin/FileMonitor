using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileMonitor.Utilities;

namespace FileMonitor
{
    public class FileDeletionService : IFileDeletionService
    {
        private readonly IFileSystemValidator _fileSystemValidator;
        private readonly ILogger _logger;
        private readonly string[] _paths = { @"C:\Users\krist\Downloads\input" };

        public FileDeletionService(ILogger logger, IFileSystemValidator fileSystemValidation)
        {
            _logger = logger;
            _fileSystemValidator = fileSystemValidation;
        }

        public void Run()
        {
            try
            {
                DeleteExpiredFilesAndEmptyFolders(_paths);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
        }

        private void DeleteExpiredFilesAndEmptyFolders(IEnumerable<string> paths)
        {
            foreach (var path in paths)
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
                    IEnumerable<FileInfo> filesToDelete;
                    try
                    {
                        filesToDelete = subFolder.EnumerateFiles()
                            .Where(x => _fileSystemValidator.FileIsValid(x.Name));
                    }
                    catch (Exception e)
                    {
                        _logger.Log(e.Message);
                        continue;
                    }

                    foreach (var fileToDelete in filesToDelete)
                    {
                        fileToDelete.Delete();
                        _logger.Log($"File: {fileToDelete.Name} - Deleted");
                    }

                    DeleteFolderIfEmpty(subFolder);
                }
            }
        }

        private void DeleteFolderIfEmpty(DirectoryInfo subFolder)
        {
            if (subFolder.EnumerateFiles().Any()) return;

            try
            {
                subFolder.Delete();
                _logger.Log($"Folder at {subFolder.FullName} - Deleted");
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
        }
    }
}