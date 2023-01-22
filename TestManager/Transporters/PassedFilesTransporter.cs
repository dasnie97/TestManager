using ProductTest.Models;
using TestManager.Helpers;
using TestManager.Interfaces;
using TestManager.Other;

namespace TestManager.Transporters;

public class PassedFilesTransporter : TransporterBase, IFileTestReportsTransporter
{
    private readonly Statistics _statistics;
    private readonly FileProcessor _fileProcessor;
    private readonly Config _config;
    public PassedFilesTransporter(Statistics statistics, FileProcessor fileProcessor, Config config)
    {
        _statistics = statistics;
        _fileProcessor = fileProcessor;
        _config = config;
    }
    public void TransportTestReports()
    {
        var fileTestReports = LoadTestReports(_config);
        foreach (var file in fileTestReports)
        {
            if (file.Status == TestStatus.Passed)
            {
                _fileProcessor.CopyFile(file);
                _fileProcessor.MoveFile(file);
                _fileProcessor.ProcessedData.Add(new TrackedTestReport(file));
                _statistics.numberOfFilesProcessed++;
            }
            else
            {
                _fileProcessor.DeleteFile(file);
            }
        }
    }
}
