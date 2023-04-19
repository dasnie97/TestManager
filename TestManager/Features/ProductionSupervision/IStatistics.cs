using TestManager.Features.TrackedTestReports;

namespace TestManager.Features.ProductionSupervision;

public interface IStatistics
{
    public int NumberOfFilesPassed { get; }
    public int NumberOfFilesFailed { get; }
    public int NumberOfFilesProcessed { get; }
    public double Yield { get; }
    public List<ITrackedTestReport> GetProcessedData();
    public void Add(ITrackedTestReport testReport);
    public void Reset();

}