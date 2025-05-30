// --------------------------------------------------------------------------------------------------------------------
// file="GenerateTokenValidationErrorResponseSwaggerExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Requests.Identity;

namespace WeatherForecasting.Api.Swagger.Examples.Identity.Responses;

public class GenerateTokenValidationErrorResponseSwaggerExample : IExamplesProvider<CommandResult>
{
    public CommandResult GetExamples()
    {
        var errors = new List<ErrorItem>
        {
            new()
            {
                PropertyName = nameof(GenerateTokenRequest.EmailAddress),
                Message = Constants.ErrorMessages.InvalidEmailAddress
            }
        };
        
        return new CommandResult(false, null, null, errors, StatusCodes.Status422UnprocessableEntity);
    }
}