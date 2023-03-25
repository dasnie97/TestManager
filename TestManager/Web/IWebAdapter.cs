using ProductTest.Models;
using TestManager.Features.Analysis;

namespace TestManager.Web;

public interface IWebAdapter
{
    TrackedTestReport CreateTrackedTestReport(FileTestReport file);
    public void FTPUpload(string filePath);
}
