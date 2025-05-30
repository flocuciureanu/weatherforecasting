using System.Text.Json.Serialization;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;

public record CoordinatesResponse
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double Longitude { get; set; }
}