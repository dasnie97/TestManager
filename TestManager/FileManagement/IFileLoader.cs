using TestEngineering.Models;

namespace TestManager.FileManagement;

public interface IFileLoader
{
    IEnumerable<string> GetFiles(string inputDirectoryPath);
    IEnumerable<FileTestReport> GetTestReportFiles(string inputDirectoryPath);
}
