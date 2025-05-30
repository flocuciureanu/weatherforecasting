// --------------------------------------------------------------------------------------------------------------------
// file="GetWeatherForecastResponseSwaggerSuccessfulExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Responses.Weather;

namespace WeatherForecasting.Api.Swagger.Examples.Weather.Responses;

public class GetWeatherForecastResponseSwaggerSuccessfulExample : IExamplesProvider<CommandResult>
{
    public CommandResult GetExamples()
    {
        var tomorrow = DateTime.UtcNow.Date.AddDays(1);

        var summaries = new List<WeatherSummaryWithDate>();
        for (var i = 0; i < 8; i++)
        {
            summaries.Add(BuildSummary(tomorrow, i));
        }
        
        var weatherForecastResponse = new GetWeatherForecastResponse(Constants.ExampleCityName, DateOnly.FromDateTime(tomorrow), summaries);

        return new CommandResult(true, weatherForecastResponse, null, [], StatusCodes.Status200OK);
    }

    private static WeatherSummaryWithDate BuildSummary(DateTime tomorrow, int i)
    {
        var weatherSummary = new WeatherSummary("Clouds: broken clouds",
            "19.01°C",
            "18.9°C",
            "17.86 - 19.65°C",  
            "74%", 
            "5.66 m/s from 230°");

        var date = tomorrow.AddHours(i * 3);
        return new WeatherSummaryWithDate(weatherSummary, date.ToString("f"));
    }
}