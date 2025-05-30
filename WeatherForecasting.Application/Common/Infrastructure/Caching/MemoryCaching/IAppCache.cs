// --------------------------------------------------------------------------------------------------------------------
// file="IAppCache.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.Caching.MemoryCaching;

public interface IAppCache
{
    T? Get<T>(string key);
    void Set<T>(string key, T value, TimeSpan duration);
}