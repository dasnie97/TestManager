using System.Net;

namespace TestManager.Web;

public interface IHTTPService
{
    public Task<List<T>> HttpGetTestReport<T>(string serialNumber);
    public Task<List<T>> HttpGetWorkstation<T>(string name);
    public Task<HttpContent> HttpPostTestReport<T>(T Product);
    public Task<HttpStatusCode> HttpPutWorkstation<T>(T Product);
}