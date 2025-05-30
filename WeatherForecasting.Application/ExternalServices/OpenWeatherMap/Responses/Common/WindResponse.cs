using System.Text.Json.Serialization;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.Common;

public record WindResponse
{
    [JsonPropertyName("speed")]
    public double Speed  { get; init; }

    [JsonPropertyName("deg")]
    public double Deg { get; init; }

    [JsonPropertyName("gust")]
    public double Gust { get; init; }
}