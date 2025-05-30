// --------------------------------------------------------------------------------------------------------------------
// file="WeatherCacheKeys.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Contracts.Enums;

namespace WeatherForecasting.Application.Features.Weather;

public static class WeatherCacheKeys
{
    public static string Current(string city, TemperatureUnit units, string languageCode)
    {
        languageCode ??= Constants.DefaultLanguageCode;
        return $"current::{city.ToLowerInvariant()}::{units.ToString("G").ToLowerInvariant()}::{languageCode.ToLowerInvariant()}";
    }

    public static string Forecast(string city, DateOnly date, TemperatureUnit units, string languageCode)
    {
        languageCode ??= Constants.DefaultLanguageCode;
        return $"forecast::{city.ToLowerInvariant()}::{date:dd-MM-yyyy}::{units.ToString("G").ToLowerInvariant()}::{languageCode.ToLowerInvariant()}";
    }
}