// --------------------------------------------------------------------------------------------------------------------
// file="MetadataController.cs">
// --------------------------------------------------------------------------------------------------------------------

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Api.CustomRateLimiterPolicies;
using WeatherForecasting.Api.Swagger.Examples.Common;
using WeatherForecasting.Api.Swagger.Examples.Metadata.Responses;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Application.Features.Metadata.Queries;

namespace WeatherForecasting.Api.Controllers;

[ApiController]
public class MetadataController : ControllerBase
{
    private readonly IMediator _mediator;

    public MetadataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route(ApiEndpoints.V1.Metadata.GetTemperatureUnits)]
    [EnableRateLimiting(RateLimiterPolicies.AuthenticatedUserPolicy)]
    [Authorize]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get supported temperature units",
        Description = "Returns a list of supported temperature units (e.g., Celsius, Fahrenheit) that can be used in weather queries.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns valid temperature unit values for that can be used for the 'units' parameters across the project", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetTemperatureUnitsResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status429TooManyRequests, "Too many requests response example. This is what a response might look like when you exceed your request allowance.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status429TooManyRequests, typeof(TooManyRequestsResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server error response example. This is what a response might look like when something goes wrong on the server side.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServerErrorResponseSwaggerExample))]
    public async Task<IActionResult> GetTemperatureUnitsAsync(CancellationToken cancellationToken)
    {
        var query = new GetTemperatureUnitsQuery();

        var commandResult = await _mediator.Send(query, cancellationToken);

        return StatusCode(commandResult.StatusCode, commandResult);
    }
    
    [HttpGet]
    [Route(ApiEndpoints.V1.Metadata.GetLanguageCodes)]
    [EnableRateLimiting(RateLimiterPolicies.AuthenticatedUserPolicy)]
    [Authorize]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get supported language codes",
        Description = "Returns a list of supported ISO language codes that can be used to localise weather descriptions.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns valid language code values that can be used for the 'languageCode' parameters across the project", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetLanguageCodesResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status429TooManyRequests, "Too many requests response example. This is what a response might look like when you exceed your request allowance.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status429TooManyRequests, typeof(TooManyRequestsResponseSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server error response example. This is what a response might look like when something goes wrong on the server side.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServerErrorResponseSwaggerExample))]
    public async Task<IActionResult> GetLanguageCodesAsync(CancellationToken cancellationToken)
    {
        var query = new GetLanguageCodesQuery();

        var commandResult = await _mediator.Send(query, cancellationToken);

        return StatusCode(commandResult.StatusCode, commandResult);
    }
}