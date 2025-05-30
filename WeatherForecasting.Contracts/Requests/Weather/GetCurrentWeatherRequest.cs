// --------------------------------------------------------------------------------------------------------------------
// file="GetCurrentWeatherRequest.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Contracts.Enums;

namespace WeatherForecasting.Contracts.Requests.Weather;

public record GetCurrentWeatherRequest(string CityName, TemperatureUnit Units, string LanguageCode);
