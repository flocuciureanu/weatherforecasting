using WeatherForecasting.Application.Common.Infrastructure.Mediator;

namespace WeatherForecasting.Application.Features.Metadata.Queries;

public record GetLanguageCodesQuery : IQuery<ICommandResult>;