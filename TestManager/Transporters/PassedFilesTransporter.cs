using ProductTest.Models;
using TestManager.Helpers;
using TestManager.Interfaces;
using TestManager.Other;

namespace TestManager.Transporters;

public class PassedFilesTransporter : TransporterBase, ITransporter
{
    public PassedFilesTransporter()
    {
        
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
                Statistics.Instance.NumberOfFilesProcessed++;
            }
            else
            {
                FileProcessor.Instance.DeleteFile(file);
            }
        }
    }
}
