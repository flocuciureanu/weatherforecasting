// --------------------------------------------------------------------------------------------------------------------
// file="QueryStringParametersList.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.HttpClients.Helpers;

public class QueryStringParametersList
{
    private readonly List<KeyValuePair<string, string>> _list = [];

    public void Add(string key, string value)
    {
        _list.Add(new KeyValuePair<string, string>(key, value));
    }

    private string GetQueryString()
    {
        return string.Join("&", _list.Select(p =>
            Uri.EscapeDataString(p.Key) + "=" + Uri.EscapeDataString(p.Value)));
    }

    public string BuildUrl(string baseUrl)
    {
        var queryString = GetQueryString();
        if (string.IsNullOrWhiteSpace(queryString))
            return baseUrl;
        
        var separator = baseUrl.Contains('?') ? "&" : "?";

        return $"{baseUrl}{separator}{queryString}";
    }
}