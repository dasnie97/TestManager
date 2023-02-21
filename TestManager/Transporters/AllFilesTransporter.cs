using ProductTest.Interfaces;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestManager.ConfigHelpers;
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

        _fileProcessor.Reset();
    }
}
