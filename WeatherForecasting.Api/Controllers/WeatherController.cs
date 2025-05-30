// --------------------------------------------------------------------------------------------------------------------
// file="WeatherController.cs">
// --------------------------------------------------------------------------------------------------------------------

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Api.CustomRateLimiterPolicies;
using WeatherForecasting.Api.Swagger.Examples.Common;
using WeatherForecasting.Api.Swagger.Examples.Weather.Responses;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Application.Features.Weather.Queries.GetCurrentWeather;
using WeatherForecasting.Application.Features.Weather.Queries.GetWeatherForecast;
using WeatherForecasting.Contracts.Enums;
using WeatherForecasting.Contracts.Requests.Weather;

namespace WeatherForecasting.Api.Controllers;

[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route(ApiEndpoints.V1.Weather.Current)]
    [EnableRateLimiting(RateLimiterPolicies.AuthenticatedUserPolicy)]
    [Authorize]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Gets the current weather conditions for the specified city.",
        Description = $"Returns current weather data for a given city. Use `/metadata/temperature-units` and `metadata/language-codes` to get valid values for 'units' and 'languageCode'.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns current weather successfully", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetCurrentWeatherResponseSwaggerSuccessfulExample))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Validation error response example. This is what a response might look like when an invalid value is submitted for 'CityName'.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(WeatherValidationErrorResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Not found response example. This is what a response might look like when no corresponding city can be found for the submitted value of 'CityName'.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(WeatherNotFoundResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status429TooManyRequests, "Too many requests response example. This is what a response might look like when you exceed your request allowance.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status429TooManyRequests, typeof(TooManyRequestsResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server error response example. This is what a response might look like when something goes wrong on the server side.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServerErrorResponseSwaggerExample))]
    public async Task<IActionResult> GetCurrentWeatherAsync(
        [FromQuery, SwaggerParameter("City to get weather for. Example: 'London', 'Rotterdam'")] string cityName,
        [FromQuery, SwaggerParameter("Optional temperature unit for the response. If left blank, Celsius will be used by default. Use the 'metadata/temperature-units' endpoint to see supported values. Example: 'Celsius', 'Fahrenheit'")] TemperatureUnit units,
        [FromQuery, SwaggerParameter("Optional language code for the response text. If left blank, English ('en') will be used by default. Use the 'metadata/language-codes' endpoint to see supported values. Example: 'en', 'nl'")] string languageCode,

        CancellationToken cancellationToken)
    {
        var request = new GetCurrentWeatherRequest(cityName, units, languageCode);
        var query = new GetCurrentWeatherQuery(request);

        var commandResult = await _mediator.Send(query, cancellationToken);

        return StatusCode(commandResult.StatusCode, commandResult);
    }

    [HttpGet]
    [Route(ApiEndpoints.V1.Weather.Forecast)]
    [EnableRateLimiting(RateLimiterPolicies.AuthenticatedUserPolicy)]
    [Authorize]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Gets the weather forecast for the specified city for up to 5 days in the future.",
        Description = $"Returns weather forecast data for a given city and date. Use `/metadata/temperature-units` and `metadata/language-codes` to get valid values for 'units' and 'languageCode'.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns weather forecast successfully", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetWeatherForecastResponseSwaggerSuccessfulExample))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Validation error response example. This is what a response might look like when an invalid value is submitted for 'CityName'.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(WeatherValidationErrorResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Not found response example. This is what a response might look like when no corresponding city can be found for the submitted value of 'CityName'.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(WeatherNotFoundResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status429TooManyRequests, "Too many requests response example. This is what a response might look like when you exceed your request allowance.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status429TooManyRequests, typeof(TooManyRequestsResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server error response example. This is what a response might look like when something goes wrong on the server side.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServerErrorResponseSwaggerExample))]
    public async Task<IActionResult> GetWeatherForecastAsync(
        [FromQuery, SwaggerParameter("City to get weather for. Example: 'London', 'Rotterdam'")] string cityName,
        [FromQuery, SwaggerParameter("Date for which to get the weather forecast. Format: 'YYYY-MM-DD'. Must be within the next 5 days.")] DateOnly forecastDate,
        [FromQuery, SwaggerParameter("Optional temperature unit for the response. If left blank, Celsius will be used by default. Use the 'metadata/temperature-units' endpoint to see supported values. Example: 'Celsius', 'Fahrenheit'")] TemperatureUnit units,
        [FromQuery, SwaggerParameter("Optional language code for the response text. If left blank, English ('en') will be used by default. Use the 'metadata/language-codes' endpoint to see supported values. Example: 'en', 'nl'")] string languageCode,
        CancellationToken cancellationToken)
    {
        var request = new GetWeatherForecastRequest(cityName, forecastDate, units, languageCode);
        var query = new GetWeatherForecastQuery(request);

        var commandResult = await _mediator.Send(query, cancellationToken);

        return StatusCode(commandResult.StatusCode, commandResult);
    }
}