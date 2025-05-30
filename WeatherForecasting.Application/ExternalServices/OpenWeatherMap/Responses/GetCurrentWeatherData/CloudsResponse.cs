// --------------------------------------------------------------------------------------------------------------------
// file="CloudsResponse.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;

public class CloudsResponse
{
    [JsonPropertyName("all")]
    public double All { get; init; }
}