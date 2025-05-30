// --------------------------------------------------------------------------------------------------------------------
// file="GetWeatherForecastQueryHandler.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WeatherForecasting.Application.Common.ExtensionMethods;
using WeatherForecasting.Application.Common.Infrastructure.Caching.MemoryCaching;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Application.Common.Infrastructure.Serialization;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetWeatherForecastData;
using WeatherForecasting.Application.Features.Weather.Mappers;
using WeatherForecasting.Contracts.Responses.Weather;

namespace WeatherForecasting.Application.Features.Weather.Queries.GetWeatherForecast;

public class GetWeatherForecastQueryHandler : IQueryHandler<GetWeatherForecastQuery, ICommandResult>
{
    private readonly IGetWeatherForecastDataService _getWeatherForecastDataService;
    private readonly IAppCache _cache;
    private readonly IAppJsonSerializer _jsonSerializer;
    private readonly ILogger<GetWeatherForecastQueryHandler> _logger;

    public GetWeatherForecastQueryHandler(IGetWeatherForecastDataService getWeatherForecastDataService, 
        IAppCache cache, 
        IAppJsonSerializer jsonSerializer, 
        ILogger<GetWeatherForecastQueryHandler> logger)
    {
        _getWeatherForecastDataService = getWeatherForecastDataService;
        _cache = cache;
        _jsonSerializer = jsonSerializer;
        _logger = logger;
    }

    public async Task<ICommandResult> Handle(GetWeatherForecastQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = WeatherCacheKeys.Forecast(query.Request.CityName, query.Request.ForecastDate, query.Request.Units, query.Request.LanguageCode);
        var response = _cache.Get<GetWeatherForecastResponse>(cacheKey);
        if (response is not null) 
            return CommandResultFactory.Success(response);
        
        var weatherForecastData = await _getWeatherForecastDataService.GetWeatherForecastDataAsync(query.Request.CityName, 
            query.Request.Units.ToOpenWeatherTemperatureUnits(), query.Request.LanguageCode, cancellationToken);
        
        if (weatherForecastData is not { Code: StatusCodes.Status200OK } || weatherForecastData.List is null || weatherForecastData.List.Count <= 0)
        {
            var request = _jsonSerializer.Serialize(query.Request);
            _logger.LogError("OpenWeatherMap returned status != 200 in Handler: {Handler} for the following request: {Request}", 
                nameof(GetWeatherForecastQueryHandler), request);
            return weatherForecastData.ToFailedWeatherForecastResponse();
        }
        
        response = weatherForecastData.ToWeatherForecastResponse(query.Request.Units, query.Request.ForecastDate);
        
        _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));

        return CommandResultFactory.Success(response);
    }
}