using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestManager.FileHelpers;

namespace TestManager.Transporters;

public class AllFilesRemover : CustomTransporter, ITransporter
{
    private readonly IFileProcessor _fileProcessor;
    public AllFilesRemover(IFileProcessor fileProcessor)
    {
        _fileProcessor = fileProcessor;
    }
    public void TransportTestReports()
    {
        var fileTestReports = LoadTestReports();
        foreach (var file in fileTestReports)
        {
            _fileProcessor.DeleteFile(file);
        }
    }
}
