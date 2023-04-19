using ProductTest.Models;
using TestManager.Features.ProductionSupervision;

namespace TestManager.Features.TrackedTestReports;

public class LocallyTrackedTestReport : ITrackedTestReport
{
    public Workstation Workstation { get; }
    public string SerialNumber { get; }
    public TestStatus Status { get; }
    public DateTime TestDateTimeStarted { get; }
    public IEnumerable<TestStep> TestSteps { get; }
    public TimeSpan TestingTime { get; }
    public string FixtureSocket { get; }
    public string Failure { get; }
    public bool IsFalseCall { get; set; }
    public bool IsFirstPass { get; set; }

    private readonly IStatistics _statistics;

    public LocallyTrackedTestReport(TestReport testReport, IStatistics statistics)
    {
        Workstation = testReport.Workstation;
        SerialNumber = testReport.SerialNumber;
        Status = testReport.Status;
        TestDateTimeStarted = testReport.TestDateTimeStarted;
        TestSteps = testReport.TestSteps;
        TestingTime = testReport.TestingTime;
        FixtureSocket = testReport.FixtureSocket;
        Failure = testReport.Failure;

        _statistics = statistics;
        SetFirstPassFlag();
        SetFalseCallFlag();
    }

    private void SetFirstPassFlag()
    {
        IsFirstPass = true;
    }
    private void SetFalseCallFlag()
    {
        IsFalseCall = true;
    }
}
