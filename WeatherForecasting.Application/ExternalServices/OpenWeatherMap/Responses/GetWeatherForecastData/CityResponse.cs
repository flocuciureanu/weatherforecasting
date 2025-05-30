using System.Text.Json.Serialization;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetWeatherForecastData;

public record CityResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }
    
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("coord")]
    public CoordinatesResponse? Coordinates { get; init; }

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("population")]
    public long Population { get; init; }
    
    [JsonPropertyName("timezone")]
    public int Timezone { get; init; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; init; }
    
    [JsonPropertyName("sunset")]
    public long Sunset { get; init; }
}