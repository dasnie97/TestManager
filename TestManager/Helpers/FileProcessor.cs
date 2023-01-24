using ProductTest.Models;

namespace TestManager.Helpers;

public sealed class FileProcessor
{
    public static FileProcessor Instance { get { return lazy.Value; } }
    public bool IsDataTransferEnabled { get; set; } = true;
    public int TransferOption { get; set; }
    public List<TrackedTestReport> ProcessedData { get; set; } = new List<TrackedTestReport>();
    private FileProcessor()
    {

    }

    private static readonly Lazy<FileProcessor> lazy = new Lazy<FileProcessor>(() => new FileProcessor());

    public void CopyFile(FileTestReport testReport)
    {
        if (Config.Instance.IsCopyingEnabled)
        {
            File.Copy(testReport.FilePath, Path.Combine(Config.Instance.DateNamedCopyDirectory, Path.GetFileName(testReport.FilePath)));
        }
    }

    public void MoveFile(FileTestReport testReport)
    {
        File.Move(testReport.FilePath, Path.Combine(Config.Instance.OutputDir, Path.GetFileName(testReport.FilePath)), true);
    }

    public void DeleteFile(FileTestReport testReport)
    {
        File.Delete(testReport.FilePath);
    }
}
