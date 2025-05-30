// --------------------------------------------------------------------------------------------------------------------
// file="GetCurrentWeatherQueryHandler.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WeatherForecasting.Application.Common.ExtensionMethods;
using WeatherForecasting.Application.Common.Infrastructure.Caching.MemoryCaching;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Application.Common.Infrastructure.Serialization;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetCurrentWeatherData;
using WeatherForecasting.Application.Features.Weather.Mappers;
using WeatherForecasting.Contracts.Responses.Weather;

namespace WeatherForecasting.Application.Features.Weather.Queries.GetCurrentWeather;

public class GetCurrentWeatherQueryHandler : IQueryHandler<GetCurrentWeatherQuery, ICommandResult>
{
    private readonly IGetCurrentWeatherDataService _getCurrentWeatherDataService;
    private readonly ILogger<GetCurrentWeatherQueryHandler> _logger;
    private readonly IAppCache _cache;
    private readonly IAppJsonSerializer _jsonSerializer;

    public GetCurrentWeatherQueryHandler(IGetCurrentWeatherDataService getCurrentWeatherDataService, 
        ILogger<GetCurrentWeatherQueryHandler> logger, 
        IAppCache cache, 
        IAppJsonSerializer jsonSerializer)
    {
        _getCurrentWeatherDataService = getCurrentWeatherDataService;
        _logger = logger;
        _cache = cache;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<ICommandResult> Handle(GetCurrentWeatherQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = WeatherCacheKeys.Current(query.Request.CityName, query.Request.Units, query.Request.LanguageCode);
        var response = _cache.Get<GetCurrentWeatherResponse>(cacheKey);
        if (response is not null) 
            return CommandResultFactory.Success(response);
        
        var currentWeatherData = await _getCurrentWeatherDataService.GetCurrentWeatherDataAsync(query.Request.CityName, 
            query.Request.Units.ToOpenWeatherTemperatureUnits(), query.Request.LanguageCode, cancellationToken);

        if (currentWeatherData is not { Code: StatusCodes.Status200OK })
        {
            var request = _jsonSerializer.Serialize(query.Request);
            _logger.LogError("OpenWeatherMap returned status != 200 in Handler: {Handler} for the following request: {Request}", 
                nameof(GetCurrentWeatherQueryHandler), request);
            return currentWeatherData.ToFailedWeatherForecastResponse();
        }
            
        response = currentWeatherData.ToCurrentWeatherResponse(query.Request.Units);
        
        _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));

        return CommandResultFactory.Success(response);
    }
}