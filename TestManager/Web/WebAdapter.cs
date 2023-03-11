using FTPPlugin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProductTest.Interfaces;
using System.Net.Http.Json;
using TestManager.Configuration;

namespace TestManager.Web;

public class WebAdapter : IWebAdapter
{
    private readonly IWebConfig _webConfig;
    private readonly IWorkstation _workstation;
    private readonly IFTPService _ftpService;
    private readonly IHTTPService _httpService;

    public WebAdapter(IWebConfig webConfig,
                        IWorkstation workstation,
                        IFTPService ftpService)
    {
        _webConfig = webConfig;
        _workstation = workstation;
        _ftpService = ftpService;
    }

    public void FTPUpload(string filePath)
    {
        if (_webConfig.SendOverFTP)
        {
            _ftpService.Upload(filePath);
        }
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
