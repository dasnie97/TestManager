using TestEngineering.DTO;
using TestEngineering.Models;
using TestManager.Models;

namespace TestManager.Interfaces;

public interface IWebAdapter
{
    public void FTPUpload(string filePath);
    public Task HTTPPost(TestReport testReport);
    public Task HTTPPost(RemoteWorkstation workstation);
    public Task HTTPPost(DowntimeReport downtimeReport);
    public Task<List<TestReportDTO>> HTTPGetTestReportsBySerialNumber(string serialNumber);
    public Task HTTPPutWorkstation(RemoteWorkstation workstation);
    public Task<List<WorkstationDTO>> HTTPGetWorkstationsByName(string name);
}
