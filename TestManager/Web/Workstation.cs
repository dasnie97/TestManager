using ProductTest.Interfaces;
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
        OperatorName = _workstationConfig.OperatorName;
    }
}
