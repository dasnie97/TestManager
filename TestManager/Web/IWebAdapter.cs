using TestManager.Features.Analysis;

namespace TestManager.Web;

public interface IWebAdapter
{
    public void FTPUpload(string filePath);
    void HTTPUpload(TrackedTestReport file);
}
