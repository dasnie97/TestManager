﻿using ProductTest.Common;
using ProductTest.Interfaces;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Web;

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