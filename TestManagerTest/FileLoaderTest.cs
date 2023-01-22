using ProductTest.Models;
using ProductTestTest;
using TestManager.Helpers;
using TestManager.Interfaces;

namespace TestManagerTest;

public class FileLoaderTest : IDisposable
{
    private string okFilePath;
    private string errorFilePath;
    public FileLoaderTest()
    {
        var okFile = RandomTestReport.ArrangeTestReportWithDefaultAndRandomData();
        okFile.SaveReport(Directory.GetCurrentDirectory());
        okFilePath = okFile.FilePath;
        var errorFile = RandomTestReport.ArrangeTestReportWithDefaultAndRandomData();
        errorFile.SaveReport(Directory.GetCurrentDirectory());
        errorFilePath = errorFile.FilePath;
    }

    public void Dispose()
    {
        if (File.Exists(okFilePath)) { File.Delete(okFilePath); }
        if (File.Exists(errorFilePath)) { File.Delete(errorFilePath); } 
    }

    [Fact]
    public void TestIfCorrectFilesAreLoaded()
    {
        IFileLoader loader = new FileLoader();
        var loadedFiles = loader.GetFiles(Directory.GetCurrentDirectory());
    }
}