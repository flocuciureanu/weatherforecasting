// --------------------------------------------------------------------------------------------------------------------
// file="OpenWeatherMapHealthCheckService.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using WeatherForecasting.Application.Common.AppSettings.OpenWeatherMap;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Engine;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Requests.HealthCheck;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.HealthCheck;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.HealthCheck;

public class OpenWeatherMapHealthCheckService : IOpenWeatherMapHealthCheckService
{
    private readonly IOpenWeatherMapEngine<OpenWeatherMapHealthCheckRequest, OpenWeatherMapHealthCheckResponse> _openWeatherMapEngine;
    private readonly OpenWeatherMapSettings _openWeatherMapSettings;

    public OpenWeatherMapHealthCheckService(IOpenWeatherMapEngine<OpenWeatherMapHealthCheckRequest, OpenWeatherMapHealthCheckResponse> openWeatherMapEngine,
        IOptions<OpenWeatherMapSettings> openWeatherMapSettings)
    {
        _openWeatherMapEngine = openWeatherMapEngine;
        _openWeatherMapSettings = openWeatherMapSettings.Value;
    }

    public async Task<OpenWeatherMapHealthCheckResponse> HealthCheckAsync(CancellationToken cancellationToken)
    {
        var apiKey = _openWeatherMapSettings.ApiKey;
        
        var request = new OpenWeatherMapHealthCheckRequest
        {
            ApiKey = apiKey,
            CityName = Constants.ExampleCityName
        };
        
        var response = await _openWeatherMapEngine.GetAsync(request, cancellationToken);
        
        return response;
    }
}