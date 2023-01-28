using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.ConfigHelpers;
using TestManager.FileHelpers;

namespace TestManager.Transporters;

public abstract class TransporterBase
{
    protected virtual IEnumerable<FileTestReport> LoadTestReports()
    {
        IFileLoader fileLoader = new FileLoader();
        IDirectoryConfig config = Config.GetInstance(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));
        IEnumerable<FileTestReport> loaded = fileLoader.GetTestReportFiles(config.InputDir);
        return loaded;
    }
}
