// --------------------------------------------------------------------------------------------------------------------
// file="IOpenWeatherMapHealthCheckService.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.HealthCheck;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.HealthCheck;

public interface IOpenWeatherMapHealthCheckService
{
    Task<OpenWeatherMapHealthCheckResponse> HealthCheckAsync(CancellationToken cancellationToken);
}