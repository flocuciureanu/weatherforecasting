// --------------------------------------------------------------------------------------------------------------------
// file="WeatherMapper.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.ExtensionMethods;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.Common;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetWeatherForecastData;
using WeatherForecasting.Contracts.Enums;
using WeatherForecasting.Contracts.Responses.Weather;

namespace WeatherForecasting.Application.Features.Weather.Mappers;

public static class WeatherMapper
{
    public static GetCurrentWeatherResponse ToCurrentWeatherResponse(this GetCurrentWeatherDataResponse weatherData, TemperatureUnit unit)
    {
        return new GetCurrentWeatherResponse(
            weatherData.Name,
            ToWeatherSummary(weatherData.Weather[0], weatherData.Main, weatherData.Wind, unit));
    }

    public static GetWeatherForecastResponse ToWeatherForecastResponse(this GetWeatherForecastDataResponse weatherData,
        TemperatureUnit unit, DateOnly forecastDate)
    {
        var summaries = new List<WeatherSummaryWithDate>();
        foreach (var item in weatherData.List!)
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(item.UnixTimeStamp).UtcDateTime;
            var dateOnly = DateOnly.FromDateTime(dateTime);
            
            if (dateOnly == forecastDate)
                summaries.Add(ToWeatherSummaryWithDate(item, dateTime, unit));
        }

        return new GetWeatherForecastResponse(weatherData.City?.Name,
            forecastDate,
            summaries);
    }

    public static ICommandResult ToFailedWeatherForecastResponse(this IOpenWeatherMapErrorResponse errorResponse)
    {
        return errorResponse.Code switch
        {
            StatusCodes.Status400BadRequest => CommandResultFactory.Failure(Constants.ErrorMessages.OpenWeatherMapInvalidCityName),
            StatusCodes.Status401Unauthorized => CommandResultFactory.Failure(Constants.ErrorMessages.OpenWeatherMapServiceUnavailable),
            StatusCodes.Status404NotFound => CommandResultFactory.NotFound(Constants.ErrorMessages.OpenWeatherMapInvalidCityName),
            StatusCodes.Status429TooManyRequests => CommandResultFactory.Failure(Constants.ErrorMessages.OpenWeatherMapServiceUnavailable),
            >= StatusCodes.Status500InternalServerError => CommandResultFactory.Failure(Constants.ErrorMessages.OpenWeatherMapServiceUnavailable),
            _ => CommandResultFactory.Failure(Constants.ErrorMessages.OpenWeatherMapServiceUnavailable)
        };
    }
    
    private static WeatherSummary ToWeatherSummary(WeatherResponse weather, MainResponse main, WindResponse wind, TemperatureUnit unit)
    {
        var temperatureUnitSymbol = unit.ToSymbol();
        
        return new WeatherSummary(
            $"{weather?.Main}: {weather?.Description}",
            $"{main?.Temperature}{temperatureUnitSymbol}",
            $"{main?.FeelsLike}{temperatureUnitSymbol}",
            $"{main?.MinTemperature} - {main?.MaxTemperature}{temperatureUnitSymbol}",
            $"{main?.Humidity}%",
            $"{wind?.Speed} m/s from {wind?.Deg}°");
    }
    
    private static WeatherSummaryWithDate ToWeatherSummaryWithDate(WeatherForecastItemResponse weatherDataItem, 
        DateTime forecastDate, TemperatureUnit unit)
    {
        var weatherSummary = ToWeatherSummary(weatherDataItem.Weather[0], weatherDataItem.Main, weatherDataItem.Wind, unit);
        
        return new WeatherSummaryWithDate(weatherSummary, forecastDate.ToString("f"));
    }
}