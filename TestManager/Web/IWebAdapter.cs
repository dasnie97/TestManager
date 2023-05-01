using TestEngineering.DTO;
using TestEngineering.Models;

namespace TestManager.Web;

public interface IWebAdapter
{
    public void FTPUpload(string filePath);
    public Task HTTPUpload(TestReport testReport);
    public Task<List<TestReportDTO>> HTTPGetTestReportsBySerialNumber(string serialNumber);
    public Task HTTPPutWorkstation(WorkstationDTO workstation);
    public Task<List<WorkstationDTO>> HTTPGetWorkstationsByName(string name);
}
