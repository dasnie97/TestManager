using ProductTest.Common;
using ProductTest.Models;
using ProductTestTest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.ConfigHelpers;
using TestManager.FileHelpers;
using TestManager.Helpers;
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
    IStatistics statistics;

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
        statistics = Statistics.GetInstance();
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

        var transporter = new TransporterFactory(fileProcessor, statistics).GetTransporter();
        transporter.TransportTestReports();
        var foundOutputTestReports = fileLoader.GetTestReportFiles(outputDir);
        var foundCopyTestReports = fileLoader.GetTestReportFiles(dateNamedCopyDir);

        Assert.Empty(Directory.GetFiles(inputDir));
        Assert.True(Directory.GetFiles(outputDir).Count() == 1);
        Assert.True(Directory.GetFiles(dateNamedCopyDir).Count() == 1);
        Assert.True(foundOutputTestReports.First().Status == TestStatus.Passed);
        Assert.True(foundCopyTestReports.First().Status == TestStatus.Passed);
    }

    [Fact]
    public void AllFilesRemoverTest()
    {
        fileProcessor.IsDataTransferEnabled = true;
        fileProcessor.TransferOption = 1;
        RecreateDirectories();
        CreateFileTestReports();

        var transporter = new TransporterFactory(fileProcessor, statistics).GetTransporter();
        transporter.TransportTestReports();

        Assert.Empty(Directory.GetFiles(inputDir));
        Assert.Empty(Directory.GetFiles(outputDir));
        Assert.Empty(Directory.GetFiles(dateNamedCopyDir));
    }

    [Fact]
    public void AllFilesTransporterTest()
    {
        fileProcessor.IsDataTransferEnabled = true;
        fileProcessor.TransferOption = 2;
        RecreateDirectories();
        CreateFileTestReports();

        var transporter = new TransporterFactory(fileProcessor, statistics).GetTransporter();
        transporter.TransportTestReports();
        var foundOutputTestReports = fileLoader.GetTestReportFiles(outputDir);
        var foundCopyTestReports = fileLoader.GetTestReportFiles(dateNamedCopyDir);

        Assert.Empty(Directory.GetFiles(inputDir));
        Assert.True(Directory.GetFiles(outputDir).Count() == 2);
        Assert.True(Directory.GetFiles(dateNamedCopyDir).Count() == 2);
        Assert.True(foundOutputTestReports.Where(testReport => testReport.Status == TestStatus.Failed).Count() == 1);
        Assert.True(foundOutputTestReports.Where(testReport => testReport.Status == TestStatus.Passed).Count() == 1);
        Assert.True(foundCopyTestReports.Where(testReport => testReport.Status == TestStatus.Failed).Count() == 1);
        Assert.True(foundCopyTestReports.Where(testReport => testReport.Status == TestStatus.Passed).Count() == 1);
    }

    [Fact]
    public void BadTransferOptionTest()
    {
        fileProcessor.IsDataTransferEnabled = true;
        fileProcessor.TransferOption = 3;
        RecreateDirectories();
        CreateFileTestReports();

        Assert.Throws<InvalidOperationException>(() => new TransporterFactory(fileProcessor, statistics).GetTransporter());
    }

    [Fact]
    public void NoFilesTransporterTest()
    {
        fileProcessor.IsDataTransferEnabled = false;
        fileProcessor.TransferOption = 2;
        RecreateDirectories();
        CreateFileTestReports();

        var transporter = new TransporterFactory(fileProcessor, statistics).GetTransporter();
        transporter.TransportTestReports();

        Assert.Empty(Directory.GetFiles(outputDir));
        Assert.Empty(Directory.GetFiles(copyDir));
        Assert.False(Directory.Exists(dateNamedCopyDir));
        Assert.True(Directory.GetFiles(inputDir).Count() == 2);
    }

    private void CreateFileTestReports()
    {
        FileTestReportCreator creator = new FileTestReportCreator();

        creator.Status= TestStatus.Passed;
        creator.SerialNumber= Guid.NewGuid().ToString();
        var passedTestReport = creator.Create();
        passedTestReport.SaveReport(inputDir);

        creator.Status = TestStatus.Failed;
        creator.SerialNumber = Guid.NewGuid().ToString();
        var failedTestReport = creator.Create();
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
