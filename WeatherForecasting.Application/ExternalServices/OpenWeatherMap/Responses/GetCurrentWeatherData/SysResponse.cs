// --------------------------------------------------------------------------------------------------------------------
// file="SysResponse.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;

public class SysResponse
{
    [JsonPropertyName("type")]
    public int Type { get; init; }
    
    [JsonPropertyName("id")]
    public long Id { get; init; }
    
    [JsonPropertyName("country")]
    public string? Country { get; init; }
    
    [JsonPropertyName("sunrise")]
    public long Sunrise { get; init; }
    
    [JsonPropertyName("sunset")]
    public long Sunset { get; init; }
}