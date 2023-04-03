using ProductTest.Models;
using TestManager.Configuration;
using TestManager.Features.Analysis;

namespace TestManager.Web;

public class WebAdapter : IWebAdapter
{
    private readonly IWebConfig _webConfig;
    private readonly IFTPService _ftpService;
    private readonly IHTTPService _httpService;

    public WebAdapter(IWebConfig webConfig,
                        IFTPService ftpService,
                        IHTTPService httpService)
    {
        _webConfig = webConfig;
        _ftpService = ftpService;
        _httpService = httpService;
    }

    public void FTPUpload(string filePath)
    {
        if (_webConfig.SendOverFTP)
        {
            _ftpService.Upload(filePath);
        }
    }

    public void HTTPUpload(TrackedTestReport testReport)
    {
        if (_webConfig.SendOverHTTP)
        {
            _httpService.HttpPost(testReport);
        }
    }

    public TrackedTestReport CreateTrackedTestReport(FileTestReport file)
    {
        TrackedTestReport trackedTestReport = new TrackedTestReport(file);

        return trackedTestReport;
    }
}
