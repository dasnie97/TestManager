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

public class AllFilesRemover : TransporterBase, ITransporter
{
    private readonly Config _config;
    public AllFilesRemover()
    {

    }
    public void TransportTestReports()
    {
        var fileTestReports = LoadTestReports();
        foreach (var file in fileTestReports)
        {
            FileProcessor.Instance.DeleteFile(file);
        }
    }
}
