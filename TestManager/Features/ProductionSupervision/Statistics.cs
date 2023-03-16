using ProductTest.Models;
using TestManager.Features.Analysis;

namespace TestManager.Features.ProductionSupervision;

public class Statistics : IStatistics
{
    public int NumberOfFilesPassed { get; private set; } = 0;
    public int NumberOfFilesFailed { get; private set; } = 0;
    public int NumberOfFilesProcessed { get; private set; } = 0;
    public double Yield { get; private set; } = 0;
    private List<TrackedTestReport> _processedData = new List<TrackedTestReport>();

    public List<TrackedTestReport> GetProcessedData()
    {
        return _processedData;
    }

    public void Add(TrackedTestReport testReport)
    {
        _processedData.Add(testReport);
        NumberOfFilesProcessed++;
        if (testReport.Status == TestStatus.Passed)
        {
            NumberOfFilesPassed++;
        }
        else
        {
            NumberOfFilesFailed++;
        }
        CalculateYield();
    }

    private void CalculateYield()
    {
        if (NumberOfFilesProcessed != 0)
        {
            Yield = (NumberOfFilesProcessed - NumberOfFilesFailed) * 100.0 / NumberOfFilesProcessed;
        }
    }

    public void Reset()
    {
        NumberOfFilesPassed = 0;
        NumberOfFilesFailed = 0;
        NumberOfFilesProcessed = 0;
        Yield = 0;
        _processedData.Clear();
    }
}
