using System.Text.Json.Serialization;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.Common;

public record WeatherResponse
{
    [JsonPropertyName("id")]
    public long Id  { get; init; }

    [JsonPropertyName("main")]
    public string? Main { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
    
    [JsonPropertyName("icon")]
    public string? Icon { get; init; }
}