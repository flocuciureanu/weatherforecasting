// --------------------------------------------------------------------------------------------------------------------
// file="OpenWeatherMapHealthCheckRequest.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.HttpClients.Helpers;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Requests.HealthCheck;

public class OpenWeatherMapHealthCheckRequest : OpenWeatherMapBaseRequest
{
    protected override string Url => $"{BaseUrl}/weather";
    
    private QueryStringParametersList GetQueryStringParameters()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
            throw new ArgumentException(Constants.ErrorMessages.NullApiKey);
        if (string.IsNullOrWhiteSpace(CityName))
            throw new ArgumentException(Constants.ErrorMessages.NullCityName);

        var stringParametersList = new QueryStringParametersList();
        stringParametersList.Add(Constants.QueryStringKeys.ApiKeyQueryKey, ApiKey);
        stringParametersList.Add(Constants.QueryStringKeys.CityNameQueryKey, CityName);

        return stringParametersList;
    }
    
    public override Uri GetUri()
    {
        var url = GetQueryStringParameters().BuildUrl(Url);

        return new Uri(url);
    }
}