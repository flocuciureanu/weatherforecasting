// --------------------------------------------------------------------------------------------------------------------
// file="GenerateTokenRequestSwaggerExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Contracts.Requests.Identity;

namespace WeatherForecasting.Api.Swagger.Examples.Identity.Requests;

public class GenerateTokenRequestSwaggerExample : IExamplesProvider<GenerateTokenRequest>
{
    public GenerateTokenRequest GetExamples()
    {
        return new GenerateTokenRequest(Constants.ExampleEmailAddress);
    }
}