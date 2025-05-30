// --------------------------------------------------------------------------------------------------------------------
// file="WeatherSummary.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Contracts.Responses.Weather;

public record WeatherSummary(
    string Description,
    string Temperature,
    string FeelsLike,
    string TemperatureRange,
    string Humidity,
    string Wind);