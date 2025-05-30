using System.Text.Json.Serialization;
using WeatherForecasting.Application.Common.Infrastructure.Serialization.Helpers;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.Common;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses.GetCurrentWeatherData;

public record GetCurrentWeatherDataResponse : IOpenWeatherMapBaseResponse, IOpenWeatherMapErrorResponse
{
    [JsonPropertyName("coord")]
    public CoordinatesResponse? Coordinates { get; init; }
    
    [JsonPropertyName("weather")]
    public IReadOnlyList<WeatherResponse>? Weather { get; init; }
    
    [JsonPropertyName("base")]
    public string? Base { get; init; }
    
    [JsonPropertyName("main")]
    public MainResponse? Main { get; init; }
    
    [JsonPropertyName("visibility")]
    public double Visibility { get; init; }
    
    [JsonPropertyName("wind")]
    public WindResponse? Wind { get; init; }
    
    [JsonPropertyName("clouds")]
    public CloudsResponse? Clouds { get; init; }
        
    [JsonPropertyName("dt")]
    public long UnixTimeStamp { get; init; }
    
    [JsonPropertyName("sys")]
    public SysResponse? Sys { get; init; }
    
    [JsonPropertyName("timezone")]
    public int Timezone { get; init; }
    
    [JsonPropertyName("id")]
    public long Id { get; init; }
    
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    
    [JsonPropertyName("cod")]
    [JsonConverter(typeof(StringAsIntConverter))]
    public int Code { get; init; }

    [JsonPropertyName("message")]
    [JsonConverter(typeof(IntAsStringConverter))]
    public string? Message { get; init; }

    [JsonPropertyName("parameters")]
    public IReadOnlyCollection<string>? Parameters { get; init; }
}