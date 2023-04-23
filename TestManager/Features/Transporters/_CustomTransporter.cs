using TestEngineering.Models;
using System.Text.RegularExpressions;
using TestManager.Configuration;
using TestManager.FileManagement;

namespace TestManager.Features.Transporters;

// NOT USED
public class _CustomTransporter
{
    private readonly IDirectoryConfig _config;

    public _CustomTransporter(IDirectoryConfig config)
    {
        _config = config;
    }
    protected IEnumerable<FileTestReport> LoadTestReports()
    {
        IFileLoader fileLoader = new FileLoader();
        IEnumerable<FileTestReport> loaded = fileLoader.GetTestReportFiles(_config.InputDir);

        foreach (var testReport in loaded)
        {
            ReplaceFileContent(testReport.FilePath);
        }

        return fileLoader.GetTestReportFiles(_config.InputDir);
    }

    private void ReplaceFileContent(string filePath)
    {
        Regex regex = new Regex("TestName.*?iot:psk:remote:encryptedsecret");
        var originalFileContent = File.ReadAllText(filePath);
        var newFileContent = regex.Replace(originalFileContent, "TestName:\tiot:psk:remote:encryptedsecret");
        File.WriteAllText(filePath, newFileContent);
    }
}
