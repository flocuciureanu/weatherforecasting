// --------------------------------------------------------------------------------------------------------------------
// file="WeatherNotFoundResponseSwaggerExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;

namespace WeatherForecasting.Api.Swagger.Examples.Weather.Responses;

public class WeatherNotFoundResponseSwaggerExample : IExamplesProvider<CommandResult>
{
    public CommandResult GetExamples()
    {
        return new CommandResult(false, null, Constants.ErrorMessages.OpenWeatherMapInvalidCityName, [], StatusCodes.Status404NotFound);
    }
}