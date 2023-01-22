using ProductTest.Common;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Helpers;

public class WebAdapter
{
    private readonly Workstation _workstation;
    public WebAdapter(Workstation workstation)
    {
        _workstation = workstation;
        Initialize();
    }

    private HTTPPlugin _httpService = new HTTPPlugin();

    private void Initialize()
    {
        if (!WorkstationExists())
        {
            PostWorkstation();
        }
    }

    private bool WorkstationExists()
    {
        var workstationsFound = _httpService.HttpGet<WorkstationBase>().Result.Where(x => x.Name == _workstation.Name).ToList();

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

    private WorkstationBase PostWorkstation()
    {
        return _httpService.HttpPost(_workstation).Result.ReadFromJsonAsync<Workstation>().Result;
    }
}
