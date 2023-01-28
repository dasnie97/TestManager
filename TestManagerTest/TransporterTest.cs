using ProductTest.Common;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.ConfigHelpers;
using TestManager.FileHelpers;
using TestManager.Transporters;

namespace TestManagerTest;

public class TransporterTest : IDisposable
{
    private string tempDir;
    private string inputDir;
    private string outputDir;
    private string copyDir;
    private string dateNamedCopyDir;
    IFileLoader fileLoader;
    IFileProcessor fileProcessor;

    public TransporterTest()
    {
        tempDir = Path.Combine(Directory.GetCurrentDirectory(), "tempTest");

        inputDir = Path.Combine(tempDir, "input");
        outputDir = Path.Combine(tempDir, "output");
        copyDir = Path.Combine(tempDir, "copy");
        RecreateDirectories();

        var configuration = ConfigurationManager.OpenExeConfiguration("testhost.dll");
        IDirectoryConfig dirConfig = Config.GetInstance(configuration);
        dirConfig.WriteConfig("InputDir", inputDir);
        dirConfig.WriteConfig("OutputDir", outputDir);
        dirConfig.WriteConfig("CopyDir", copyDir);
        dateNamedCopyDir = dirConfig.DateNamedCopyDirectory;

        fileProcessor = FileProcessor.GetInstance(dirConfig);
        fileLoader = new FileLoader();
    }

    public void Dispose()
    {
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public void PassedFilesTransporterTest()
    {
        fileProcessor.IsDataTransferEnabled = true;
        fileProcessor.TransferOption = 0;
        RecreateDirectories();
        CreateFileTestReports();

        var transporter = new TransporterFactory(fileProcessor).GetTransporter();
        transporter.TransportTestReports();
        var foundOutputTestReports = fileLoader.GetTestReportFiles(outputDir);
        var foundCopyTestReports = fileLoader.GetTestReportFiles(dateNamedCopyDir);

        Assert.Empty(Directory.GetFiles(inputDir));
        Assert.True(Directory.GetFiles(outputDir).Count() == 1);
        Assert.True(Directory.GetFiles(dateNamedCopyDir).Count() == 1);
        Assert.True(foundOutputTestReports.First().Status == TestStatus.Passed);
        Assert.True(foundCopyTestReports.First().Status == TestStatus.Passed);
    }

    private void CreateFileTestReports()
    {
        var passedTestReport = FileTestReport.Create("123123123", 
                                                     "TestW", 
                                                     new List<TestStepBase>() 
                                                     {
                                                         TestStep.Create("Test1", DateTime.Now, TestStatus.Passed)
                                                     });
        passedTestReport.SaveReport(inputDir);
        var failedTestReport = FileTestReport.Create("456456456",
                                                     "TestW",
                                                     new List<TestStepBase>()
                                                     {
                                                         TestStep.Create("Test1", DateTime.Now, TestStatus.Failed)
                                                     });
        failedTestReport.SaveReport(inputDir);
    }

    private void RecreateDirectories()
    {
        if (Directory.Exists(inputDir)) Directory.Delete(inputDir, true);
        if (Directory.Exists(outputDir)) Directory.Delete(outputDir, true);
        if (Directory.Exists(copyDir)) Directory.Delete(copyDir, true);

        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);
        Directory.CreateDirectory(copyDir);
    }
}
