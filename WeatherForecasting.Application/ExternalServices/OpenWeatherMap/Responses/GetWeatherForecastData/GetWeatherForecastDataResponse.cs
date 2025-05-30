using System.Text.Json.Serialization;
using WeatherForecasting.Application.Common.Infrastructure.Serialization.Helpers;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetWeatherForecastData;

public record GetWeatherForecastDataResponse : IOpenWeatherMapBaseResponse, IOpenWeatherMapErrorResponse
{
    [JsonPropertyName("cod")]
    [JsonConverter(typeof(StringAsIntConverter))]
    public int Code { get; init; }
    
    [JsonPropertyName("message")]
    [JsonConverter(typeof(IntAsStringConverter))]
    public string? Message { get; init; }

    [JsonPropertyName("parameters")]
    public IReadOnlyCollection<string>? Parameters { get; init; }

    [JsonPropertyName("cnt")]
    public int Count { get; init; }
    
    [JsonPropertyName("list")]
    public IReadOnlyCollection<WeatherForecastItemResponse>? List { get; init; }
    
    [JsonPropertyName("city")]
    public CityResponse? City { get; init; }
}