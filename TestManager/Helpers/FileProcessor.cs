using ProductTest.Models;

namespace TestManager.Helpers;

public class FileProcessor
{
    public bool IsDataTransferEnabled { get; set; } = true;
    public int TransferOption { get; set; }
    public List<TrackedTestReport> ProcessedData { get; set; } = new List<TrackedTestReport>();
    public FileProcessor(Config configLoader)
    {
        _cfg = configLoader;
    }

    protected readonly Config _cfg;        

    public void CopyFile(FileTestReport testReport)
    {
        if (_cfg.IsCopyingEnabled)
        {
            File.Copy(testReport.FilePath, Path.Combine(_cfg.DateNamedCopyDirectory, Path.GetFileName(testReport.FilePath)));
        }
    }

    public void MoveFile(FileTestReport testReport)
    {
        File.Move(testReport.FilePath, Path.Combine(_cfg.OutputDir, Path.GetFileName(testReport.FilePath)), true);
    }

    public void DeleteFile(FileTestReport testReport)
    {
        File.Delete(testReport.FilePath);
    }
}
