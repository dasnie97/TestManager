using ProductTest.Interfaces;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestManager.Helpers;
using TestManager.Interfaces;

namespace TestManager.Transporters;

public class AllFilesTransporter : TransporterBase, ITransporter
{
    private readonly Statistics _statistics;
    public AllFilesTransporter(Statistics statistics)
    {
        _statistics = statistics;
    }
    public void TransportTestReports()
    {
        var fileTestReports = LoadTestReports();
        foreach (var file in fileTestReports)
        {
            FileProcessor.Instance.CopyFile(file);
            FileProcessor.Instance.MoveFile(file);
            FileProcessor.Instance.ProcessedData.Add(new TrackedTestReport(file));
            _statistics.numberOfFilesProcessed++;
            if (file.Status != TestStatus.Passed)
                _statistics.numberOfFilesFailed++;

        }
    }
}
