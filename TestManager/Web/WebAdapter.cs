using FTPPlugin;
using Microsoft.Extensions.Configuration;
using ProductTest.Common;
using ProductTest.Interfaces;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Web;

//TODO: When to use FTP service in transportation?
public class WebAdapter : IWebAdapter
{
    private readonly IConfiguration _config;
    private readonly IFTPService _ftpService;
    private readonly IHTTPService _httpService;
    private readonly IWorkstation _workstation;

    public WebAdapter(IConfiguration config, IWorkstation workstation)
    {
        _config = config;
        _ftpService = new FTPService(_config);
        _workstation = workstation;
    }

    public void FTPUpload(string filePath)
    {
         _ftpService.Upload(filePath);
    }

    private void Initialize()
    {
        if (!WorkstationExists())
        {
            PostWorkstation();
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
