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

public class AllFilesRemover : TransporterBase, IFileTestReportsTransporter
{
    private readonly FileProcessor _fileProcessor;
    private readonly Config _config;
    public AllFilesRemover(FileProcessor fileProcessor, Config config)
    {
        _fileProcessor = fileProcessor;
        _config = config;
    }
    public void TransportTestReports()
    {
        var fileTestReports = LoadTestReports(_config);
        foreach (var file in fileTestReports)
        {
            _fileProcessor.DeleteFile(file);
        }
    }
}
