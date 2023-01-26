using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Helpers;
using TestManager.Interfaces;

namespace TestManager.Transporters;

public abstract class TransporterBase
{
    protected virtual IEnumerable<FileTestReport> LoadTestReports()
    {
        IFileLoader fileLoader = new FileLoader();
        IEnumerable<FileTestReport> loaded = fileLoader.GetTestReportFiles(Config.Instance.InputDir);
        return loaded;
    }
}
