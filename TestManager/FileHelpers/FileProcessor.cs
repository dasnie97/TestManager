using ProductTest.Models;
using System.Configuration;
using TestManager.ConfigHelpers;
using TestManager.Helpers;

namespace TestManager.FileHelpers;

public sealed class FileProcessor : IFileProcessor
{
    public bool IsDataTransferEnabled { get; set; } = true;
    public int TransferOption { get; set; }
    private readonly IDirectoryConfig _config;
    private FileProcessor(IDirectoryConfig config)
    {
        _config = config;
    }

    public static FileProcessor GetInstance(IDirectoryConfig configuration)
    {
        return new Lazy<FileProcessor>(() => new FileProcessor(configuration)).Value;
    }

    public void CopyFile(FileTestReport testReport)
    {
        if (_config.IsCopyingEnabled)
        {
            File.Copy(testReport.FilePath, Path.Combine(_config.DateNamedCopyDirectory, Path.GetFileName(testReport.FilePath)));
        }
    }

    public void MoveFile(FileTestReport testReport)
    {
        File.Move(testReport.FilePath, Path.Combine(_config.OutputDir, Path.GetFileName(testReport.FilePath)), true);
    }

    public void DeleteFile(FileTestReport testReport)
    {
        File.Delete(testReport.FilePath);
    }
}
