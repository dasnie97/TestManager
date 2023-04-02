using TestManager.Features.ProductionSupervision;
using TestManager.FileManagement;
using TestManager.Features.Analysis;
using TestManager.Web;
using ProductTest.Models;

namespace TestManager.Features.Transporters;

public class AllFilesTransporter : ITransporter
{
    public event EventHandler FileTransported;

    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;
    private readonly IWebAdapter _webAdapter;

    public AllFilesTransporter(IFileProcessor fileProcessor, IStatistics statistics, IWebAdapter webAdapter)
    {
        _fileProcessor = fileProcessor;
        _statistics = statistics;
        _webAdapter = webAdapter;
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
        TrackedTestReport trackedTestReport = _webAdapter.CreateTrackedTestReport(file);
        _webAdapter.HTTPUpload(trackedTestReport);
        _statistics.Add(trackedTestReport);
        _fileProcessor.CopyFile(file);
        _fileProcessor.MoveFile(file);
    }
}
