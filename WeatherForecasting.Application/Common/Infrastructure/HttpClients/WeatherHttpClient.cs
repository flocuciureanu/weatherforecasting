// --------------------------------------------------------------------------------------------------------------------
// file="WeatherHttpClient.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.HttpClients;

public class WeatherHttpClient
{
    private readonly HttpClient _httpClient;

    public WeatherHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> GetStringAsync(Uri uri, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(uri, cancellationToken);
        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        return responseString;
    }
    
    public async Task<HttpResponseMessage> HeadAsync(Uri uri, CancellationToken cancellationToken)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Head, uri);
        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        return response;
    }
}