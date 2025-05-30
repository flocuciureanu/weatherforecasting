// --------------------------------------------------------------------------------------------------------------------
// file="IdentityController.cs">
// --------------------------------------------------------------------------------------------------------------------

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Api.Swagger.Examples.Identity.Requests;
using WeatherForecasting.Api.Swagger.Examples.Identity.Responses;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Application.Features.Identity.Commands;
using WeatherForecasting.Contracts.Requests.Identity;

namespace WeatherForecasting.Api.Controllers;

[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route(ApiEndpoints.V1.Identity.GenerateToken)]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Generate identity token",
        Description = "Mimics a sign-in endpoint to issue a JWT token for testing purposes. Use the returned token in the Authorization header as 'Bearer {token}'.")]
    [SwaggerRequestExample(typeof(GenerateTokenRequest), typeof(GenerateTokenRequestSwaggerExample))]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns jwt token", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GenerateTokenResponseSwaggerSuccessfulExample))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Validation error response example. This is what a response might look like when an invalid 'emailAddress' is submitted.", typeof(CommandResult))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(GenerateTokenValidationErrorResponseSwaggerExample))]
    public async Task<IActionResult> GenerateTokenAsync([FromBody] GenerateTokenRequest request, CancellationToken cancellationToken)
    {
        var command = new GenerateTokenCommand(request);
        
        var commandResult = await _mediator.Send(command, cancellationToken);
        
        return StatusCode(commandResult.StatusCode, commandResult);
    }
}