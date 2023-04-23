using TestEngineering.Models;
using TestManager.Configuration;
using TestManager.Features.ProductionSupervision;
using TestManager.Web;

namespace TestManager.Features.TrackedTestReports;

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
        if (_webConfig.SendOverHTTP)
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
