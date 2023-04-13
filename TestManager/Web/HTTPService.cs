using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;
using System.Web;

namespace TestManager.Web;

public class HTTPService : IHTTPService
{
    private readonly IConfiguration _config;
    private HttpClient _httpClient;
    private bool connectionEstablished = false;

    public HTTPService(IConfiguration configuration)
    {
        _config = configuration;
    }

    public async Task<HttpContent> HttpPost<T>(T Product)
    {
        if (!connectionEstablished)
        {
            InitializeConnection();
        }

        var result = await CreateProductAsync<T>(Product).ConfigureAwait(false);
        return result;
    }

    public async Task<List<T>> HttpGet<T>(string serialNumber)
    {
        if (!connectionEstablished)
        {
            InitializeConnection();
        }

        var data = await GetProductAsync<T>(serialNumber).ConfigureAwait(false);
        return data;
    }

    public async Task<HttpStatusCode> HttpPut<T>(T Product)
    {
        if (!connectionEstablished)
        {
            InitializeConnection();
        }

        var response = await UpdateProductAsync<T>(Product).ConfigureAwait(false);
        return response;
    }

    private async Task<List<T>> GetProductAsync<T>(string serialNumber)
    {
        string query = BuildQuery(serialNumber);
        HttpResponseMessage response = await _httpClient.GetAsync($"api/TestReport?{query}").ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<T>>().ConfigureAwait(false);
        return data;
    }

    private async Task<HttpContent> CreateProductAsync<T>(T Product)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync<T>(
            $"api/TestReport", Product).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        // return URI of the created resource.
        return response.Content;
    }

    private async Task<HttpStatusCode> UpdateProductAsync<T>(T Product)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync<T>($"api/{typeof(T).Name}", Product).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return response.StatusCode;
    }

    private void InitializeConnection()
    {
        var section = _config.GetRequiredSection("WebAPI");
        var uri = section.GetValue<string>("URI");

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(uri);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        connectionEstablished = true;
    }

    private string BuildQuery(string serialNumber)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["serialNumber"] = serialNumber;
        return query.ToString();
    }
}

