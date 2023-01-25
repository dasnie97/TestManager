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
using TestManager.Other;

namespace TestManager.Transporters;

public class AllFilesTransporter : CustomTransporter, ITransporter
{
    public AllFilesTransporter()
    {

    }
    public void TransportTestReports()
    {
        var fileTestReports = LoadTestReports();
        foreach (var file in fileTestReports)
        {
            FileProcessor.Instance.CopyFile(file);
            FileProcessor.Instance.MoveFile(file);
            FileProcessor.Instance.ProcessedData.Add(new TrackedTestReport(file));
            Statistics.Instance.NumberOfFilesProcessed++;
            if (file.Status != TestStatus.Passed)
                Statistics.Instance.NumberOfFilesFailed++;

        }
    }
}
