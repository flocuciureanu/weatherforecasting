// --------------------------------------------------------------------------------------------------------------------
// file="GetLanguageCodesResponseSwaggerExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Responses.Metadata;

namespace WeatherForecasting.Api.Swagger.Examples.Metadata.Responses;

public class GetLanguageCodesResponseSwaggerExample : IExamplesProvider<CommandResult>
{
    public CommandResult GetExamples()
    {
        var temperatureUnits = new List<MetadataItemResponse>
        {
            new(Constants.DefaultLanguageCode, "English"),
            new("nl", "Dutch"),
        };
        
        var response = new GetMetadataResponse(temperatureUnits);
        
        return new CommandResult(true, response, null, [], StatusCodes.Status200OK);
    }
}