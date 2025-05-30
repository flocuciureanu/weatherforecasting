// --------------------------------------------------------------------------------------------------------------------
// file="ServerErrorResponseSwaggerExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;

namespace WeatherForecasting.Api.Swagger.Examples.Common;

public class ServerErrorResponseSwaggerExample : IExamplesProvider<CommandResult>
{
    public CommandResult GetExamples()
    {
        return new CommandResult(false, null, Constants.ErrorMessages.ServerError, [], StatusCodes.Status500InternalServerError);
    }
}