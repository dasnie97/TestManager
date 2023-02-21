using ProductTest.Common;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Helpers;

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
        TestReportBase testReport1 = TestReport.Create("123123123", "Test1", new List<TestStepBase>
                                                                        {TestStep.Create("Test1", DateTime.Now, TestStatus.Passed)});
        TestReportBase testReport2 = TestReport.Create("123123123", "Test2", new List<TestStepBase>
                                                                        {TestStep.Create("Test2", DateTime.Now, TestStatus.Passed)});
        TestReportBase testReport3 = TestReport.Create("123123123", "Test3", new List<TestStepBase>
                                                                        {TestStep.Create("Test3", DateTime.Now, TestStatus.Failed)});

        _statistics.Add(new TrackedTestReport(testReport1));
        _statistics.Add(new TrackedTestReport(testReport2));
        _statistics.Add(new TrackedTestReport(testReport3));
    }
}
