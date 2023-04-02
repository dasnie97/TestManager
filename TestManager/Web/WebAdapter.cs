using ProductTest.Common;
using ProductTest.Interfaces;
using ProductTest.Models;
using System.Net.Http.Json;
using TestManager.Configuration;
using TestManager.Features.Analysis;

namespace TestManager.Web;

public class WebAdapter : IWebAdapter
{
    private readonly IWebConfig _webConfig;
    private readonly IWorkstation _workstation;
    private readonly IFTPService _ftpService;
    private readonly IHTTPService _httpService;

    public WebAdapter(IWebConfig webConfig,
                        IWorkstation workstation,
                        IFTPService ftpService,
                        IHTTPService httpService)
    {
        _webConfig = webConfig;
        _workstation = workstation;
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
            _httpService.HttpPost<TestReport>(testReport);
        }
    }

    public TrackedTestReport CreateTrackedTestReport(FileTestReport file)
    {
        TrackedTestReport trackedTestReport = new TrackedTestReport(file);

        return trackedTestReport;
    }

    private bool WorkstationExists()
    {
        var workstationsFound = _httpService.HttpGet<Workstation>().Result.Where(x => x.Name == _workstation.Name).ToList();

        if (workstationsFound.Count == 0)
        {
            return false;
        }
        else if (workstationsFound.Count == 1)
        {
            return true;
        }
        else
        {
            throw new Exception("Invalid workstations existing!");
        }
    }

    private IWorkstation PostWorkstation()
    {
        return _httpService.HttpPost(_workstation).Result.ReadFromJsonAsync<Workstation>().Result;
    }
}
