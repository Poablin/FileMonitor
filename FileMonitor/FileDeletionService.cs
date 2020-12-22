using FileMonitor.Utilities;
using System;
using System.Collections.Generic;
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
            try
            {
                DeleteExpiredFilesAndEmptyFolders(_paths);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
        }

        private void DeleteExpiredFilesAndEmptyFolders(string[] _paths)
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