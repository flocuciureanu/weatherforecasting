// --------------------------------------------------------------------------------------------------------------------
// file="WeatherValidationErrorResponseSwaggerExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Requests.Weather;

namespace WeatherForecasting.Api.Swagger.Examples.Weather.Responses;

public class WeatherValidationErrorResponseSwaggerExample : IExamplesProvider<CommandResult>
{
    public CommandResult GetExamples()
    {
        var errors = new List<ErrorItem>
        {
            new()
            {
                PropertyName = nameof(GetCurrentWeatherRequest.CityName),
                Message = Constants.ErrorMessages.InvalidCityName
            }
        };
        
        return new CommandResult(false, null, Constants.ErrorMessages.BadRequest, errors, StatusCodes.Status422UnprocessableEntity);
    }
}