// --------------------------------------------------------------------------------------------------------------------
// file="GetTemperatureUnitsResponseSwaggerExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.ExtensionMethods;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Enums;
using WeatherForecasting.Contracts.Responses.Metadata;

namespace WeatherForecasting.Api.Swagger.Examples.Metadata.Responses;

public class GetTemperatureUnitsResponseSwaggerExample : IExamplesProvider<CommandResult>
{
    public CommandResult GetExamples()
    {
        var temperatureUnits = new List<MetadataItemResponse>
        {
            new(TemperatureUnit.Celsius.ToString("G"), TemperatureUnit.Celsius.GetDescription()),
            new(TemperatureUnit.Fahrenheit.ToString("G"), TemperatureUnit.Fahrenheit.GetDescription()),
            new(TemperatureUnit.Kelvin.ToString("G"), TemperatureUnit.Kelvin.GetDescription()),
        };
        
        var response = new GetMetadataResponse(temperatureUnits);
        
        return new CommandResult(true, response, null, [], StatusCodes.Status200OK);
    }
}