using System.Text.Json.Serialization;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.Common;

public record MainResponse
{
    [JsonPropertyName("temp")]
    public double Temperature { get; init; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; init; }

    [JsonPropertyName("temp_min")]
    public double MinTemperature  { get; init; }
    
    [JsonPropertyName("temp_max")]
    public double MaxTemperature { get; init; }
    
    [JsonPropertyName("pressure")]
    public double Pressure { get; init; }
    
    [JsonPropertyName("humidity")]
    public double Humidity { get; init; }
    
    [JsonPropertyName("sea_level")]
    public double SeaLevel { get; init; }
    
    [JsonPropertyName("grnd_level")]
    public double GroundLevel { get; init; }
}