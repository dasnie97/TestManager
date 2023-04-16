using ProductTest.DTO;
using ProductTest.Models;

namespace TestManager.Web;

public interface IWebAdapter
{
    public void FTPUpload(string filePath);
    public Task HTTPUpload(TestReport file);
    public TestReportDTO HTTPGetTestReport(string serialNumber);
    public Task HTTPPutWorkstation(WorkstationDTO workstation);
    public Task<List<WorkstationDTO>> HTTPGetWorkstation(string name);
}
