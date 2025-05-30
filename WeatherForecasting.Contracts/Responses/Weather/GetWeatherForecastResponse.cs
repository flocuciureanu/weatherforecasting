// --------------------------------------------------------------------------------------------------------------------
// file="GetWeatherForecastResponse.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Contracts.Responses.Weather;

public record GetWeatherForecastResponse(
    string? CityName,
    DateOnly ForecastDate,
    IEnumerable<WeatherSummaryWithDate> ThreeHourForecastWeatherSummaries);
