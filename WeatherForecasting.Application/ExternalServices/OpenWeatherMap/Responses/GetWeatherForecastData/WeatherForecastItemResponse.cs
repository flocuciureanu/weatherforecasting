using System.Text.Json.Serialization;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.Common;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetWeatherForecastData;

public record WeatherForecastItemResponse
{
    [JsonPropertyName("dt")]
    public long UnixTimeStamp { get; init; }
    
    [JsonPropertyName("main")]
    public MainResponse? Main { get; init; }

    [JsonPropertyName("weather")]
    public IReadOnlyList<WeatherResponse>? Weather { get; init; }

    [JsonPropertyName("clouds")]
    public CloudsResponse? Clouds { get; init; }

    [JsonPropertyName("wind")]
    public WindResponse? Wind { get; init; }

    [JsonPropertyName("visibility")]
    public double Visibility { get; init; }
    
    [JsonPropertyName("dt_txt")]
    public string? DateTime { get; init; }
}