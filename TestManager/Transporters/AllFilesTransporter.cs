using ProductTest.Interfaces;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestManager.FileHelpers;
using TestManager.Helpers;

namespace TestManager.Transporters;

public class AllFilesTransporter : CustomTransporter, ITransporter
{
    private readonly IFileProcessor _fileProcessor;
    public AllFilesTransporter(IFileProcessor fileProcessor)
    {
        _fileProcessor = fileProcessor;
    }
    public void TransportTestReports()
    {
        var fileTestReports = LoadTestReports();
        foreach (var file in fileTestReports)
        {
            _fileProcessor.CopyFile(file);
            _fileProcessor.MoveFile(file);
            _fileProcessor.ProcessedData.Add(new TrackedTestReport(file));
            Statistics.Instance.NumberOfFilesProcessed++;
            if (file.Status != TestStatus.Passed)
                Statistics.Instance.NumberOfFilesFailed++;

        }
    }
}
