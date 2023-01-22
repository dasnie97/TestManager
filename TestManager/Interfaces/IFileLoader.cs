using ProductTest.Common;
using ProductTest.Models;

namespace TestManager.Interfaces;

public interface IFileLoader
{
    IEnumerable<string> GetFiles(string inputDirectoryPath);
    IEnumerable<FileTestReport> GetTestReportFiles(string inputDirectoryPath);
}
