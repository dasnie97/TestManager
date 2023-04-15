using TestManager.Features.ProductionSupervision;
using TestManager.FileManagement;
using TestManager.Features.Analysis;
using TestManager.Web;
using ProductTest.Models;
using TestManager.Features.TrackedTestReports;

namespace TestManager.Features.Transporters;

public class AllFilesTransporter : ITransporter
{
    public event EventHandler FileTransported;

    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;
    private readonly IWebAdapter _webAdapter;
    private readonly ITestReportTracker _tracker;

    public AllFilesTransporter(IFileProcessor fileProcessor, IStatistics statistics, IWebAdapter webAdapter, ITestReportTracker tracker)
    {
        _fileProcessor = fileProcessor;
        _statistics = statistics;
        _webAdapter = webAdapter;
        _tracker = tracker;
    }

    public void TransportTestReports()
    {
        var fileTestReports = _fileProcessor.LoadFiles();

        foreach (var file in fileTestReports)
        {
            ProcessFiles(file);
            FileTransported?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ProcessFiles(FileTestReport file)
    {
        _webAdapter.FTPUpload(file.FilePath);
        _webAdapter.HTTPUpload(file);
        ITrackedTestReport trackedReport = _tracker.CreateTrackedTestReport(file);
        _statistics.Add(trackedReport);
        _fileProcessor.CopyFile(file);
        _fileProcessor.MoveFile(file);
    }
}
