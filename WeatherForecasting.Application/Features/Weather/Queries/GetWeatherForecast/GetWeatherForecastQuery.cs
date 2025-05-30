// --------------------------------------------------------------------------------------------------------------------
// file="GetWeatherForecastQuery.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Requests.Weather;

namespace WeatherForecasting.Application.Features.Weather.Queries.GetWeatherForecast;

public record GetWeatherForecastQuery(GetWeatherForecastRequest Request) : IQuery<ICommandResult>;
