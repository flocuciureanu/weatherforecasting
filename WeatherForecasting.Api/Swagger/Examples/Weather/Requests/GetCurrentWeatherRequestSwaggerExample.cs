// --------------------------------------------------------------------------------------------------------------------
// file="GetCurrentWeatherRequestSwaggerExample.cs">
// --------------------------------------------------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Filters;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Contracts.Requests.Weather;

namespace WeatherForecasting.Api.Swagger.Examples.Weather.Requests;

public class GetCurrentWeatherRequestSwaggerExample : IExamplesProvider<GetCurrentWeatherRequest>
{
    public GetCurrentWeatherRequest GetExamples()
    {
        return new GetCurrentWeatherRequest(Constants.ExampleCityName, Constants.DefaultTemperatureUnit, Constants.DefaultLanguageCode);
    }
}