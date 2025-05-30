// --------------------------------------------------------------------------------------------------------------------
// file="IGenericAppSettings.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.AppSettings;

public interface IGenericAppSettings<out T> where T : class, IAppSettings
{
    T? GetAppSettings();
    string? GetAppSettingValue(string key);
}