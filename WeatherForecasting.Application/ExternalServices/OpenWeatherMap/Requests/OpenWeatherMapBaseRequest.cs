// --------------------------------------------------------------------------------------------------------------------
// file="OpenWeatherMapBaseRequest.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Constants;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Requests;

public class OpenWeatherMapBaseRequest
{
    public string? ApiKey { get; init; }
    public string? CityName { get; init; }
    public string? Units { get; set; }
    public string? Language { get; set; }
    
    protected virtual string? Url { get; }
    protected static string BaseUrl => Constants.Urls.OpenWeatherMapBaseUrl;

    public virtual Uri GetUri()
    {
        return new Uri(BaseUrl);
    }
}