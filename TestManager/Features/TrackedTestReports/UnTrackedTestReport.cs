using ProductTest.Models;
using TestManager.Features.ProductionSupervision;

namespace TestManager.Features.TrackedTestReports;

public class UnTrackedTestReport : ITrackedTestReport
{
    public bool IsFirstPass { get; set; }
    public bool IsFalseCall { get; set; }
    public Workstation Workstation {get;}
    public string SerialNumber { get; }
    public TestStatus Status { get; }
    public DateTime TestDateTimeStarted { get; }
    public IEnumerable<TestStep> TestSteps { get; }
    public TimeSpan TestingTime { get; }
    public string FixtureSocket { get; }
    public string Failure { get; }

    public UnTrackedTestReport(TestReport testReport)
    {
        Workstation = testReport.Workstation;
        SerialNumber = testReport.SerialNumber;
        Status = testReport.Status;
        TestDateTimeStarted = testReport.TestDateTimeStarted;
        TestSteps = testReport.TestSteps;
        TestingTime = testReport.TestingTime;
        FixtureSocket = testReport.FixtureSocket;
        Failure = testReport.Failure;

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
