// --------------------------------------------------------------------------------------------------------------------
// file="GetCurrentWeatherResponseSwaggerSuccessfulExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Responses.Weather;

namespace WeatherForecasting.Api.Swagger.Examples.Weather.Responses;

public class GetCurrentWeatherResponseSwaggerSuccessfulExample : IExamplesProvider<CommandResult>
{
    public CommandResult GetExamples()
    {
        var weatherSummary = new WeatherSummary("Clouds: broken clouds",
            "19.01°C",
            "18.9°C",
            "17.86 - 19.65°C",  
            "74%", 
            "5.66 m/s from 230°");
        
        var currentWeatherResponse = new GetCurrentWeatherResponse(Constants.ExampleCityName, weatherSummary);

        return new CommandResult(true, currentWeatherResponse, null, [], StatusCodes.Status200OK);
    }
}