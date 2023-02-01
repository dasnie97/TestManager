namespace TestManager.Helpers;

public interface IStatistics
{
    public int NumberOfFilesPassed { get; set; }
    public int NumberOfFilesFailed { get; set; }
    public int NumberOfFilesProcessed { get; set; }
    public void Reset();
    public List<TrackedTestReport> ProcessedData { get; set; }
}