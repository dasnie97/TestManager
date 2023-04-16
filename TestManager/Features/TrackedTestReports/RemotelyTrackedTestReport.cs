using ProductTest.Models;
using TestManager.Features.ProductionSupervision;
using TestManager.Web;

namespace TestManager.Features.TrackedTestReports;

public class RemotelyTrackedTestReport : ITrackedTestReport
{
    public bool IsFirstPass { get; set; }
    public bool IsFalseCall { get; set; }
    public ProductTest.Models.Workstation Workstation {get;}
    public string SerialNumber { get; }
    public TestStatus Status { get; }
    public DateTime TestDateTimeStarted { get; }
    public IEnumerable<TestStep> TestSteps { get; }
    public TimeSpan TestingTime { get; }
    public string FixtureSocket { get; }
    public string Failure { get; }

    private readonly IStatistics _statistics;
    private readonly IWebAdapter _webAdapter;

    public RemotelyTrackedTestReport(TestReport testReport, IStatistics statistics, IWebAdapter webAdapter)
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
        _webAdapter = webAdapter;
        SetFirstPassFlag();
        SetFalseCallFlag();
    }


    private void SetFirstPassFlag()
    {
        var remoteTestReport = _webAdapter.HTTPGet(SerialNumber);
        IsFirstPass = remoteTestReport.IsFirstPass;
    }
    private void SetFalseCallFlag()
    {
        IsFalseCall = true;
    }
}
