// --------------------------------------------------------------------------------------------------------------------
// file="AppCache.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Caching.Memory;

namespace WeatherForecasting.Application.Common.Infrastructure.Caching.MemoryCaching;

public class AppCache(IMemoryCache memoryCache) : IAppCache
{
    public T? Get<T>(string key) => 
        memoryCache.TryGetValue(key, out T? value) ? value : default;


    public void Set<T>(string key, T value, TimeSpan duration) =>
        memoryCache.Set(key, value, duration);
}