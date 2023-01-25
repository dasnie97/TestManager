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

public class AllFilesRemover : CustomTransporter, ITransporter
{
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
