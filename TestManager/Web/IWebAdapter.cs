using ProductTest.DTO;
using TestManager.Features.Analysis;

namespace TestManager.Web;

public interface IWebAdapter
{
    public void FTPUpload(string filePath);
    public void HTTPUpload(TrackedTestReport file);
    public TestReportDTO HTTPGet(string serialNumber);
}
