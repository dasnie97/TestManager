using ProductTest.Models;
using TestManager.FileHelpers;
using TestManager.Helpers;

namespace TestManager.Transporters;

public class PassedFilesTransporter : CustomTransporter, ITransporter
{
    private readonly IFileProcessor _fileProcessor;
    public PassedFilesTransporter(IFileProcessor fileProcessor)
    {
        _fileProcessor = fileProcessor;
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
                _fileProcessor.ProcessedData.Add(new TrackedTestReport(file));
                Statistics.Instance.NumberOfFilesProcessed++;
            }
            else
            {
                _fileProcessor.DeleteFile(file);
            }
        }
    }
}
