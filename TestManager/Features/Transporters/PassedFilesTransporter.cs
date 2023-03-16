using ProductTest.Models;
using TestManager.Features.ProductionSupervision;
using TestManager.FileManagement;
using TestManager.Features.Analysis;
using TestManager.Web;

namespace TestManager.Features.Transporters;

public class PassedFilesTransporter : ITransporter
{
    public event EventHandler FileTransported;

    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;
    private readonly IWebAdapter _webAdapter;

    public PassedFilesTransporter(IFileProcessor fileProcessor, IStatistics statistics, IWebAdapter webAdapter)
    {
        _fileProcessor = fileProcessor;
        _statistics = statistics;
        _webAdapter = webAdapter;
    }

    public void TransportTestReports()
    {
        var _fileTestReports = _fileProcessor.LoadFiles();

        foreach (var file in _fileTestReports)
        {
            if (file.Status == TestStatus.Passed)
            {
                ProcessFile(file);
                FileTransported?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                RemoveFile(file);
            }
        }
    }

    private void ProcessFile(FileTestReport file)
    {
        _webAdapter.FTPUpload(file.FilePath);
        _statistics.Add(new TrackedTestReport(file));
        _fileProcessor.CopyFile(file);
        _fileProcessor.MoveFile(file);
    }

    private void RemoveFile(FileTestReport file)
    {
        _fileProcessor.DeleteFile(file);
    }
}
