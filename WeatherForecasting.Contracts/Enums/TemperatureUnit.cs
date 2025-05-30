// --------------------------------------------------------------------------------------------------------------------
// file="TemperatureUnit.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace WeatherForecasting.Contracts.Enums;

public enum TemperatureUnit
{
    [Description("Celsius (°C)")]
    Celsius = 10,

    [Description("Fahrenheit (°F)")]
    Fahrenheit = 20,

    [Description("Kelvin (K)")]
    Kelvin = 30
}