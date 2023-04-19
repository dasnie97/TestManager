using ProductTest.Models;
using TestManager.Features.ProductionSupervision;
using TestManager.Features.Analysis;
using TestManager.Features.TrackedTestReports;

namespace TestManagerTest;

public class StatisticsTest
{
    private IStatistics _statistics;

	public StatisticsTest()
	{
		_statistics = new Statistics();
	}

	[Fact]
	public void YieldComputingTest()
    {
        GenerateStatistics();

        Assert.Equal(1, _statistics.NumberOfFilesFailed);
        Assert.Equal(2, _statistics.NumberOfFilesPassed);
        Assert.Equal(3, _statistics.NumberOfFilesProcessed);
        Assert.Equal(3, _statistics.GetProcessedData().Count);
        Assert.Equal(200.0 / 3.0, _statistics.Yield);
    }

    [Fact]
    public void ResetTest()
    {
        GenerateStatistics();

        _statistics.Reset();

        Assert.Equal(0, _statistics.NumberOfFilesFailed);
        Assert.Equal(0, _statistics.NumberOfFilesPassed);
        Assert.Equal(0, _statistics.NumberOfFilesProcessed);
        Assert.Empty( _statistics.GetProcessedData());
        Assert.Equal(0, _statistics.Yield);
    }

    private void GenerateStatistics()
    {
        TestReport testReport1 = new TestReport("123123123", new Workstation("Test1"), new List<TestStep>
                                                              {new TestStep("Test1", DateTime.Now, TestStatus.Passed)});
        TestReport testReport2 = new TestReport("123123123", new Workstation("Test2"), new List<TestStep>
                                                              {new TestStep("Test2", DateTime.Now, TestStatus.Passed)});
        TestReport testReport3 = new TestReport("123123123", new Workstation("Test3"), new List<TestStep>
                                                              {new TestStep("Test3", DateTime.Now, TestStatus.Failed)});

        TestReportTracker tracker = new TestReportTracker();

        _statistics.Add(tracker.CreateUnTrackedTestReport(testReport1));
        _statistics.Add(tracker.CreateUnTrackedTestReport(testReport2));
        _statistics.Add(tracker.CreateUnTrackedTestReport(testReport3));
    }
}
