// --------------------------------------------------------------------------------------------------------------------
// file="GenericAppSettings.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;

namespace WeatherForecasting.Application.Common.AppSettings;

public class GenericAppSettings<T> : IGenericAppSettings<T> where T : class, IAppSettings
{
    private readonly IConfiguration _configuration;

    public GenericAppSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public T? GetAppSettings()
    {
        return _configuration.GetSection(typeof(T).Name).Get<T>();
    }

    public string? GetAppSettingValue(string key)
    {
        return _configuration[key];
    }
}