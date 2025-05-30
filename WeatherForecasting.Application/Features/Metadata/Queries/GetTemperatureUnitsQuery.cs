// --------------------------------------------------------------------------------------------------------------------
// file="GetTemperatureUnitsQuery.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Infrastructure.Mediator;

namespace WeatherForecasting.Application.Features.Metadata.Queries;

public record GetTemperatureUnitsQuery : IQuery<ICommandResult>;
