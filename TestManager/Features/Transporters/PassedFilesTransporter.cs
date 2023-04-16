using ProductTest.Models;
using TestManager.Features.ProductionSupervision;
using TestManager.FileManagement;
using TestManager.Web;
using TestManager.Features.TrackedTestReports;

namespace TestManager.Features.Transporters;

public class PassedFilesTransporter : ITransporter
{
    public event EventHandler FileTransported;

    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;
    private readonly IWebAdapter _webAdapter;
    private readonly ITestReportTracker _tracker;

    public PassedFilesTransporter(IFileProcessor fileProcessor, IStatistics statistics, IWebAdapter webAdapter, ITestReportTracker tracker)
    {
        _fileProcessor = fileProcessor;
        _statistics = statistics;
        _webAdapter = webAdapter;
        _tracker = tracker;
    }

    public Task TransportTestReports()
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
        return Task.CompletedTask;
    }

    private void ProcessFile(FileTestReport file)
    {
        _webAdapter.FTPUpload(file.FilePath);
        _webAdapter.HTTPUpload(file);
        _fileProcessor.CopyFile(file);
        _fileProcessor.MoveFile(file);

        ITrackedTestReport trackedReport = _tracker.CreateTrackedTestReport(file);
        _statistics.Add(trackedReport);
    }

    private void RemoveFile(FileTestReport file)
    {
        _fileProcessor.DeleteFile(file);
    }
}
