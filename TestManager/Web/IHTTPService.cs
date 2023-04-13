namespace TestManager.Web;

public interface IHTTPService
{
    public Task<List<T>> HttpGet<T>(string serialNumber);
    public Task<HttpContent> HttpPost<T>(T Product);
}