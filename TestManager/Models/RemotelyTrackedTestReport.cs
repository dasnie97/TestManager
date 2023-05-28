using TestEngineering.DTO;
using TestEngineering.Exceptions;
using TestEngineering.Models;
using TestManager.Interfaces;

namespace TestManager.Models;

public class RemotelyTrackedTestReport : ITrackedTestReport
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


    private async Task SetFirstPassFlag()
    {
        var response = await _webAdapter.HTTPGetTestReportsBySerialNumber(SerialNumber);
        if (response.Count == 0)
        {
            throw new TestReportNotFoundException($"Test report '{SerialNumber}' does not exist in data base!");
        }
        else
        {
            TestReportDTO testResult = response.OrderByDescending(t => t.RecordCreated).FirstOrDefault();
            IsFirstPass = testResult.IsFirstPass;
        }
    }
    private void SetFalseCallFlag()
    {
        IsFalseCall = true;
    }
}
