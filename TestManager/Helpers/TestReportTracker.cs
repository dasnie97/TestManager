using TestEngineering.Models;
using TestManager.Configuration;
using TestManager.Interfaces;
using TestManager.Models;

namespace TestManager.Helpers;

public class TestReportTracker : ITestReportTracker
{
    private readonly IWebConfig _webConfig;
    private readonly IStatistics _statistics;
    private readonly IWebAdapter _webAdapter;

    public TestReportTracker(IWebConfig webConfig, IStatistics statistics, IWebAdapter webAdapter)
    {
        _webConfig = webConfig;
        _statistics = statistics;
        _webAdapter = webAdapter;
    }
    public TestReportTracker()
    {

    }

    public ITrackedTestReport CreateTrackedTestReport(TestReport testReport)
    {
        if (_webConfig.SendToWebAPI)
        {
            return new RemotelyTrackedTestReport(testReport, _statistics, _webAdapter);
        }
        else
        {
            return new LocallyTrackedTestReport(testReport, _statistics);
        }
    }

    public ITrackedTestReport CreateUnTrackedTestReport(TestReport testReport)
    {
        return new UnTrackedTestReport(testReport);
    }
}
