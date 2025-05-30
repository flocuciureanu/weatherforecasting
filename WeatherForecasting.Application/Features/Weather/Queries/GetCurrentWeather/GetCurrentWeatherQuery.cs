// --------------------------------------------------------------------------------------------------------------------
// file="GetCurrentWeatherQuery.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Requests.Weather;

namespace WeatherForecasting.Application.Features.Weather.Queries.GetCurrentWeather;

public record GetCurrentWeatherQuery(GetCurrentWeatherRequest Request) : IQuery<ICommandResult>;
