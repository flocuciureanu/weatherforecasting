// --------------------------------------------------------------------------------------------------------------------
// file="GetWeatherForecastRequest.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Contracts.Enums;

namespace WeatherForecasting.Contracts.Requests.Weather;

public record GetWeatherForecastRequest(string CityName, DateOnly ForecastDate, TemperatureUnit Units, string LanguageCode);
