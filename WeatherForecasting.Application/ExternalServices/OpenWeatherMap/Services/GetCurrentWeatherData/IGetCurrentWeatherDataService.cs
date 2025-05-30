// --------------------------------------------------------------------------------------------------------------------
// file="IGetCurrentWeatherDataService.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetCurrentWeatherData;

public interface IGetCurrentWeatherDataService
{
    Task<GetCurrentWeatherDataResponse> GetCurrentWeatherDataAsync(string cityName, string units, 
        string language = Constants.DefaultLanguageCode, CancellationToken cancellationToken = default);
}