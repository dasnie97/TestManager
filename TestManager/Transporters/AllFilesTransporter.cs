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

public class AllFilesTransporter : TransporterBase, IFileTestReportsTransporter
{
    private readonly Statistics _statistics;
    private readonly FileProcessor _fileProcessor;
    private readonly Config _config;
    public AllFilesTransporter(Statistics statistics, FileProcessor fileProcessor, Config config)
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
            _fileProcessor.CopyFile(file);
            _fileProcessor.MoveFile(file);
            _fileProcessor.ProcessedData.Add(new TrackedTestReport(file));
            _statistics.numberOfFilesProcessed++;
            if (file.Status != TestStatus.Passed)
                _statistics.numberOfFilesFailed++;

        }
    }
}
