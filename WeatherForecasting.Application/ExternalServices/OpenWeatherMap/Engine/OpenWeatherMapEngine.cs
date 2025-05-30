// --------------------------------------------------------------------------------------------------------------------
// file="OpenWeatherMapEngine.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Infrastructure.HttpClients;
using WeatherForecasting.Application.Common.Infrastructure.Serialization;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Requests;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Engine;

public class OpenWeatherMapEngine<TRequest, TResponse> : IOpenWeatherMapEngine<TRequest, TResponse>
    where TRequest : OpenWeatherMapBaseRequest, new()
    where TResponse : IOpenWeatherMapBaseResponse
{
    private readonly WeatherHttpClient _httpClient;
    private readonly IAppJsonSerializer _jsonSerializer;

    public OpenWeatherMapEngine(WeatherHttpClient httpClient, 
        IAppJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }
    
    public async Task<TResponse> GetAsync(TRequest request, CancellationToken cancellationToken)
    {
        var responseString = await _httpClient.GetStringAsync(request.GetUri(), cancellationToken);

        return HandleResponse(responseString);
    }

    public async Task<HttpResponseMessage> HeadAsync(TRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.HeadAsync(request.GetUri(), cancellationToken);
        
        return response;
    }

    private TResponse HandleResponse(string responseString)
    {
        var response = _jsonSerializer.Deserialize<TResponse>(responseString);
        return response;
    }
}