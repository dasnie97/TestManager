using TestEngineering.DTO;
using TestEngineering.Models;

namespace TestManager.Web;

public interface IWebAdapter
{
    public void FTPUpload(string filePath);
    public Task HTTPUpload(TestReport testReport);
    public TestReportDTO HTTPGetTestReport(string serialNumber);
    public Task HTTPPutWorkstation(WorkstationDTO workstation);
    public Task<List<WorkstationDTO>> HTTPGetWorkstations(string name);
}
