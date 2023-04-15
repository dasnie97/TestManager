using ProductTest.DTO;
using ProductTest.Models;
using TestManager.Features.Analysis;

namespace TestManager.Web;

public interface IWebAdapter
{
    public void FTPUpload(string filePath);
    public void HTTPUpload(TestReport file);
    public TestReportDTO HTTPGet(string serialNumber);
}
