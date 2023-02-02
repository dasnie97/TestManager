using ProductTest.Models;
using TestManager.FileHelpers;
using TestManager.Helpers;

namespace TestManager.Transporters;

public class PassedFilesTransporter : CustomTransporter, ITransporter
{
    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;
    public PassedFilesTransporter(IFileProcessor fileProcessor, IStatistics statistics)
    {
        _fileProcessor = fileProcessor;
        _statistics = statistics;
    }
    public void TransportTestReports()
    {
        var fileTestReports = LoadTestReports();
        foreach (var file in fileTestReports)
        {
            if (file.Status == TestStatus.Passed)
            {
                _fileProcessor.CopyFile(file);
                _fileProcessor.MoveFile(file);
                _statistics.Add(new TrackedTestReport(file));
            }
            else
            {
                _fileProcessor.DeleteFile(file);
            }
        }
    }
}
