// --------------------------------------------------------------------------------------------------------------------
// file="ErrorItem.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.Mediator;

public record ErrorItem
{
    public required string PropertyName { get; init; }
    public required string Message { get; init; }   
}