// --------------------------------------------------------------------------------------------------------------------
// file="GetCurrentWeatherDataService.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using WeatherForecasting.Application.Common.AppSettings.OpenWeatherMap;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Engine;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Requests.GetCurrentWeatherData;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetCurrentWeatherData;

public class GetCurrentWeatherDataService : IGetCurrentWeatherDataService
{
    private readonly IOpenWeatherMapEngine<GetCurrentWeatherDataRequest, GetCurrentWeatherDataResponse> _getCurrentWeatherEngine;
    private readonly OpenWeatherMapSettings _openWeatherMapSettings;

    public GetCurrentWeatherDataService(IOpenWeatherMapEngine<GetCurrentWeatherDataRequest, GetCurrentWeatherDataResponse> getCurrentWeatherEngine, 
        IOptions<OpenWeatherMapSettings> openWeatherMapSettings)
    {
        _getCurrentWeatherEngine = getCurrentWeatherEngine;
        _openWeatherMapSettings = openWeatherMapSettings.Value;
    }

    public async Task<GetCurrentWeatherDataResponse> GetCurrentWeatherDataAsync(string cityName, string units, 
        string language = Constants.DefaultLanguageCode, CancellationToken cancellationToken = default)
    {
        var apiKey = _openWeatherMapSettings.ApiKey;
        
        var request = new GetCurrentWeatherDataRequest
        {
            CityName = cityName,
            ApiKey = apiKey,
            Units = units,
            Language = language
        };
        
        var response = await _getCurrentWeatherEngine.GetAsync(request, cancellationToken);
        
        return response;
    }
}