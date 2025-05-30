// --------------------------------------------------------------------------------------------------------------------
// file="GenerateTokenResponseSwaggerSuccessfulExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Responses.Identity;

namespace WeatherForecasting.Api.Swagger.Examples.Identity.Responses;

public class GenerateTokenResponseSwaggerSuccessfulExample : IExamplesProvider<CommandResult>
{
    public CommandResult GetExamples()
    {
        const string token = "my-jwt-token";
        var response = new GenerateTokenResponse(token);
        
        return new CommandResult(true, response, null, [], StatusCodes.Status200OK);
    }
}