// --------------------------------------------------------------------------------------------------------------------
// file="ICommandResult.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.Mediator;

public interface ICommandResult
{
    bool Success { get; }
    object? Value { get; }
    string? Message { get; }
    IEnumerable<ErrorItem> Errors { get; }
    int StatusCode { get; }
}