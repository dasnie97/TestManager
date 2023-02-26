namespace TestManager.Web;

public interface IHTTPService
{
    public Task<List<T>> HttpGet<T>();
    public Task<HttpContent> HttpPost<T>(T Product);
}