// --------------------------------------------------------------------------------------------------------------------
// file="TemperatureUnitExtensions.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Contracts.Enums;

namespace WeatherForecasting.Application.Common.ExtensionMethods;

public static class TemperatureUnitExtensions
{
    public static string ToOpenWeatherTemperatureUnits(this TemperatureUnit unit) =>
        unit switch
        {
            TemperatureUnit.Celsius => "metric",
            TemperatureUnit.Fahrenheit => "imperial",
            TemperatureUnit.Kelvin => "standard",
            _ => "metric"
        };
    
    public static string ToSymbol(this TemperatureUnit unit) =>
        unit switch
        {
            TemperatureUnit.Celsius => "°C",
            TemperatureUnit.Fahrenheit => "°F",
            TemperatureUnit.Kelvin => "K",
            _ => "°C"
        };
}