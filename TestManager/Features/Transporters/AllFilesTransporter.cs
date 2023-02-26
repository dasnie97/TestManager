using TestManager.Features.ProductionSupervision;
using TestManager.FileManagement;
using TestManager.Features.Analysis;
using TestManager.Web;

namespace TestManager.Features.Transporters;

public class AllFilesTransporter : ITransporter
{
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
            _webAdapter.FTPUpload(file.FilePath);
            _statistics.Add(new TrackedTestReport(file));
            _fileProcessor.CopyFile(file);
            _fileProcessor.MoveFile(file);
        }
    }
}
