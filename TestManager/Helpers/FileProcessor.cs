using ProductTest.Common;
using ProductTest.Models;
using TestManager.Interfaces;

namespace TestManager.Helpers;

public class FileProcessor
{
    public bool IsDataTransferEnabled { get; set; }
    public int TransferOption { get; set; } = 0;
    public List<TrackedTestReport> ProcessedData { get; set; } = new List<TrackedTestReport>();
    public FileProcessor(ConfigHandler configLoader, Statistics statistics)
    {
        _cfg = configLoader;
        _statistics = statistics;
    }
    private IFileLoader _fileLoader = new FileLoader();
    private readonly Statistics _statistics;
    private readonly ConfigHandler _cfg;        
    private Workstation workstation;

    public void ProcessFiles()
    {
        var testReports = _fileLoader.GetTestReportFiles(_cfg.InputDir);
        TransferFiles(testReports);
    }

    private async Task TransferFiles(IEnumerable<FileTestReport> testReports)
    {
        FilterFilesBasingOnTransferringOption(testReports);
        if (_cfg.CopyDir != String.Empty)
        {
            CopyFiles(testReports);
        }
        MoveFiles(testReports);
        UpdateStatistics(testReports);
        
    }


    private void FilterFilesBasingOnTransferringOption(IEnumerable<FileTestReport> testReports)
    {
        switch (TransferOption)
        {
            case 0:
                testReports.Where(testReport => testReport.Status != TestStatus.Passed).ToList().
                    ForEach(testReport => File.Delete(testReport.FilePath));
                break;
            case 1:
                testReports.ToList().ForEach(testReport => File.Delete(testReport.FilePath));
                break;
            case 2:
                break;
            default:
                throw new Exception("File transfer option has invalid value!");
        }
        TransferOption = -1;
    }

    private void UpdateStatistics(IEnumerable<TestReportBase> testReports)
    {
        foreach (var testReport in testReports)
        {
            var trackedTestReport = (TrackedTestReport)testReport;
            ProcessedData.Add(trackedTestReport);
            _statistics.numberOfFilesProcessed++;
            if (testReport.Status != TestStatus.Passed)
                _statistics.numberOfFilesFailed++;
        }
    }

    private void CopyFiles(IEnumerable<FileTestReport> testReports)
    {
        var cdir = Path.Combine(_cfg.CopyDir, $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString("00")}");
        Directory.CreateDirectory(cdir);
        testReports.ToList().ForEach(testReport => File.Copy(testReport.FilePath, Path.Combine(cdir, Path.GetFileName(testReport.FilePath))));
    }

    private void MoveFiles(IEnumerable<FileTestReport> testReports)
    {
        testReports.ToList().ForEach(testReport => File.Move(testReport.FilePath, Path.Combine(_cfg.OutputDir, Path.GetFileName(testReport.FilePath)), true));
    }
}
