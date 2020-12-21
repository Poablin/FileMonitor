using System.IO;
using System.Linq;
using FileMonitor.Utilities;

namespace FileMonitor
{
    public class FileDeletionService : IFileDeletionService
    {
        private readonly IFileSystemValidator _fileSystemValidator;
        private readonly ILogger _logger;
        private string[] _paths = {@"C:\Users\krist\Downloads\input"};

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
                    {
                        var test = fileToDelete;
                        test.Delete();
                    }

                    DeleteFolderIfEmpty(subFolder);
                }
            }
        }

        public void DeleteFolderIfEmpty(DirectoryInfo subFolder)
        {
            if (!subFolder.EnumerateFiles().Any()) subFolder.Delete();
        }
    }
}