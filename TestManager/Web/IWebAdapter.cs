using ProductTest.Common;
using ProductTest.Models;
using TestManager.Features.Analysis;

namespace TestManager.Web;

public interface IWebAdapter
{
    TrackedTestReport CreateTrackedTestReport(FileTestReport file);
    public void FTPUpload(string filePath);
    void HTTPUpload(TestReport file);
}
