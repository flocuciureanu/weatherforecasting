// --------------------------------------------------------------------------------------------------------------------
// file="IOpenWeatherMapEngine.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Requests;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Responses;

namespace WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Engine;

public interface IOpenWeatherMapEngine<in TRequest, TResponse>
    where TRequest : OpenWeatherMapBaseRequest, new()
    where TResponse : IOpenWeatherMapBaseResponse
{
    Task<TResponse> GetAsync(TRequest request, CancellationToken cancellationToken);
    Task<HttpResponseMessage> HeadAsync(TRequest request, CancellationToken cancellationToken);
}