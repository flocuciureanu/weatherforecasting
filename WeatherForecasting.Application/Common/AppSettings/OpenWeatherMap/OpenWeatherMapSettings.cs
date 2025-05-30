// --------------------------------------------------------------------------------------------------------------------
// file="OpenWeatherMapSettings.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.AppSettings.OpenWeatherMap;

public class OpenWeatherMapSettings : IAppSettings
{
    public required string ApiKey { get; init; }
    public required List<string> SupportedLanguages { get; init; }
}