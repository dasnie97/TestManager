using TestManager.FileHelpers;
using TestManager.Helpers;

namespace TestManager.Transporters;

public class AllFilesTransporter : ITransporter
{
    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;
    public AllFilesTransporter(IFileProcessor fileProcessor, IStatistics statistics)
    {
        _fileProcessor = fileProcessor;
        _statistics = statistics;
    }
    public void TransportTestReports()
    {
        var fileTestReports = _fileProcessor.LoadFiles();

        foreach (var file in fileTestReports)
        {
            _fileProcessor.CopyFile(file);
            _fileProcessor.MoveFile(file);
            _statistics.Add(new TrackedTestReport(file));
        }
    }
}
