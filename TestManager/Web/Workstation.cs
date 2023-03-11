using ProductTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Configuration;

namespace TestManager.Web;

public class Workstation : IWorkstation
{
    private readonly IWorkstationConfig _workstationConfig;

    public string Name { get; set ; }
    public string OperatorName { get; set ; }

    public Workstation(IWorkstationConfig workstationConfig)
    {
        _workstationConfig = workstationConfig;
        Name = _workstationConfig.TestStationName;
    }
}
