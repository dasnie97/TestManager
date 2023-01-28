using ProductTest.Common;
using ProductTest.Models;
using ProductTestTest;
using TestManager.FileHelpers;
using TestManager.Helpers;

namespace TestManagerTest;

public class FileLoaderTest : IDisposable
{
    private string tempDir;
    private string okFilePath;
    private string emptyFilePath;
    public FileLoaderTest()
    {
        tempDir = Path.Combine(Directory.GetCurrentDirectory(), "tempDir");
        Directory.CreateDirectory(tempDir);

        var okFile = RandomTestReport.ArrangeTestReportWithDefaultAndRandomData();
        okFile.SaveReport(tempDir);
        okFilePath = okFile.FilePath;

        var emptyFile = FileTestReport.Create("123123123",
                                            "TestW",
                                            new List<TestStepBase>()
                                            {
                                                TestStep.Create("Test1", DateTime.Now, TestStatus.Passed)
                                            });
        emptyFilePath = Path.Combine(tempDir, emptyFile.FileName);
        File.Create(emptyFilePath).Close();
    }

    public void Dispose()
    {
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public void TestIfCorrectFilesAreLoaded()
    {
        IFileLoader loader = new FileLoader();

        var loadedFiles = loader.GetFiles(tempDir);

        Assert.True(loadedFiles.Count() == 1);
        Assert.Equal(loadedFiles.First(), okFilePath);
    }
}