// --------------------------------------------------------------------------------------------------------------------
// file="GetCurrentWeatherDataRequest.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.HttpClients.Helpers;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Requests.GetCurrentWeatherData;

public class GetCurrentWeatherDataRequest : OpenWeatherMapBaseRequest
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
        
        if (!string.IsNullOrWhiteSpace(Language))
            stringParametersList.Add(Constants.QueryStringKeys.LanguageQueryKey, Language);
        
        if (!string.IsNullOrWhiteSpace(Units))
            stringParametersList.Add(Constants.QueryStringKeys.UnitsQueryKey, Units);
        
        return stringParametersList;
    }
    
    public override Uri GetUri()
    {
        var url = GetQueryStringParameters().BuildUrl(Url);

        return new Uri(url);
    }
}