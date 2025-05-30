// --------------------------------------------------------------------------------------------------------------------
// file="IGetWeatherForecastDataService.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetWeatherForecastData;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetWeatherForecastData;

public interface IGetWeatherForecastDataService
{
    Task<GetWeatherForecastDataResponse> GetWeatherForecastDataAsync(string cityName, string units,
        string language = Constants.DefaultLanguageCode, CancellationToken cancellationToken = default);
}