// --------------------------------------------------------------------------------------------------------------------
// file="GetWeatherForecastDataService.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using WeatherForecasting.Application.Common.AppSettings.OpenWeatherMap;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Engine;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Requests.GetWeatherForecastData;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetWeatherForecastData;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetWeatherForecastData;

public class GetWeatherForecastDataService : IGetWeatherForecastDataService
{
    private readonly IOpenWeatherMapEngine<GetWeatherForecastDataRequest, GetWeatherForecastDataResponse> _getWeatherForecastService;
    private readonly OpenWeatherMapSettings _openWeatherMapSettings;

    public GetWeatherForecastDataService(IOpenWeatherMapEngine<GetWeatherForecastDataRequest, GetWeatherForecastDataResponse> getWeatherForecastService, 
        IOptions<OpenWeatherMapSettings> openWeatherMapSettings)
    {
        _getWeatherForecastService = getWeatherForecastService;
        _openWeatherMapSettings = openWeatherMapSettings.Value;
    }

    public async Task<GetWeatherForecastDataResponse> GetWeatherForecastDataAsync(string cityName, string units,
        string language = Constants.DefaultLanguageCode, CancellationToken cancellationToken = default)
    {
        var apiKey = _openWeatherMapSettings.ApiKey;

        var request = new GetWeatherForecastDataRequest
        {
            CityName = cityName,
            ApiKey = apiKey,
            Units = units,
            Language = language
        };
        
        var response = await _getWeatherForecastService.GetAsync(request, cancellationToken);
        
        return response;
    }
}