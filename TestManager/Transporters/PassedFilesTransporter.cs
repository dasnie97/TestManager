using ProductTest.Models;
using TestManager.Helpers;
using TestManager.Interfaces;
using TestManager.Other;

namespace TestManager.Transporters;

public class PassedFilesTransporter : TransporterBase, ITransporter
{
    private readonly Statistics _statistics;
    public PassedFilesTransporter(Statistics statistics)
    {
        _statistics = statistics;
    }
    public void TransportTestReports()
    {
        var fileTestReports = LoadTestReports();
        foreach (var file in fileTestReports)
        {
            if (file.Status == TestStatus.Passed)
            {
                FileProcessor.Instance.CopyFile(file);
                FileProcessor.Instance.MoveFile(file);
                FileProcessor.Instance.ProcessedData.Add(new TrackedTestReport(file));
                _statistics.numberOfFilesProcessed++;
            }
            else
            {
                FileProcessor.Instance.DeleteFile(file);
            }
        }
    }
}
