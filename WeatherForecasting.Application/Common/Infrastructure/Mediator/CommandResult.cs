// --------------------------------------------------------------------------------------------------------------------
// file="CommandResult.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.Mediator;

public class CommandResult : ICommandResult
{
    public bool Success { get; }
    public object? Value { get; }
    public string? Message { get; }
    public IEnumerable<ErrorItem> Errors { get; }
    public int StatusCode { get; }

    public CommandResult(bool success, object? value, string? message, IEnumerable<ErrorItem>? errors, int statusCode)
    {
        Success = success;
        Value = value;
        Message = message;
        Errors = errors ?? [];
        StatusCode = statusCode;
    }
}