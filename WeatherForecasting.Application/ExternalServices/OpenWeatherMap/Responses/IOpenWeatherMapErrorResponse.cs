// --------------------------------------------------------------------------------------------------------------------
// file="IOpenWeatherMapErrorResponse.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses;

public interface IOpenWeatherMapErrorResponse
{
    int Code { get; init; }
    string? Message { get; init; }
    IReadOnlyCollection<string>? Parameters { get; init; }
}