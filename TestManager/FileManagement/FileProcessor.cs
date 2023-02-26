using ProductTest.Models;
using TestManager.Configuration;

namespace TestManager.FileManagement;

public class FileProcessor : IFileProcessor
{
    private readonly IDirectoryConfig _config;

    public FileProcessor(IDirectoryConfig config)
    {
        _config = config;
    }

    public IEnumerable<FileTestReport> LoadFiles()
    {
        IFileLoader fileLoader = new FileLoader();
        IEnumerable<FileTestReport> loaded = fileLoader.GetTestReportFiles(_config.InputDir);
        return loaded;
    }

    public string CopyFile(FileTestReport testReport)
    {
        var copiedFilePath = string.Empty;
        if (_config.IsCopyingEnabled)
        {
            var destinationFileName = Path.Combine(_config.DateNamedCopyDirectory, Path.GetFileName(testReport.FilePath));
            File.Copy(testReport.FilePath, destinationFileName);
            copiedFilePath = destinationFileName;
        }
        return copiedFilePath;
    }

    public string MoveFile(FileTestReport testReport)
    {
        var destinationFilePath = Path.Combine(_config.OutputDir, Path.GetFileName(testReport.FilePath));
        File.Move(testReport.FilePath, destinationFilePath, true);
        return destinationFilePath;
    }

    public void DeleteFile(FileTestReport testReport)
    {
        File.Delete(testReport.FilePath);
    }
}
