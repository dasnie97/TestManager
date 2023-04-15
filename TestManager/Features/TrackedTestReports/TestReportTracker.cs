using ProductTest.Models;
using TestManager.Configuration;
using TestManager.Features.ProductionSupervision;

namespace TestManager.Features.TrackedTestReports;

public class TestReportTracker : ITestReportTracker
{
    private readonly IWebConfig _webConfig;
    private readonly IStatistics _statistics;

    public TestReportTracker(IWebConfig webConfig, IStatistics statistics)
    {
        _webConfig = webConfig;
        _statistics = statistics;
    }
    public TestReportTracker()
    {
        
    }

    public ITrackedTestReport CreateTrackedTestReport(TestReport testReport)
    {
        if (_webConfig.SendOverHTTP)
        {
            return new RemotelyTrackedTestReport(testReport, _statistics);
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
