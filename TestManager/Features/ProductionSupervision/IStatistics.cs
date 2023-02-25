using TestManager.Features.Analysis;

namespace TestManager.Features.ProductionSupervision;

public interface IStatistics
{
    public int NumberOfFilesPassed { get; }
    public int NumberOfFilesFailed { get; }
    public int NumberOfFilesProcessed { get; }
    public double Yield { get; }
    public List<TrackedTestReport> GetProcessedData();
    public void Add(TrackedTestReport testReport);
    public void Reset();

}