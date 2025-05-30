// --------------------------------------------------------------------------------------------------------------------
// file="CommandResultFactory.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace WeatherForecasting.Application.Common.Infrastructure.Mediator;

public static class CommandResultFactory
{
    public static ICommandResult Success(object value, string? message = null, int statusCode = StatusCodes.Status200OK)
    {
        return new CommandResult(true, value, message, null, statusCode);
    }
    
    public static ICommandResult Failure(string message = Constants.Constants.ErrorMessages.BadRequest, 
        IEnumerable<ErrorItem>? errors = null, int statusCode = StatusCodes.Status400BadRequest)
    {
        return new CommandResult(false, null, message, errors, statusCode);
    }
    
    public static ICommandResult ValidationFailure(IEnumerable<ErrorItem>? errors, 
        string message = Constants.Constants.ErrorMessages.ValidationFailureMessage, int statusCode = StatusCodes.Status422UnprocessableEntity)
    {
        return new CommandResult(false, null, message, errors, statusCode);
    }

    public static ICommandResult NotFound(string message = Constants.Constants.ErrorMessages.NotFound)
    {
        return new CommandResult(false, null, message, null, 404);
    }

    public static ICommandResult Unauthorized(string message = Constants.Constants.ErrorMessages.Unauthorized)
    {
        return new CommandResult(false, null, message, null, 401);
    }

    public static ICommandResult ServerError(string message = Constants.Constants.ErrorMessages.ServerError)
    {
        return new CommandResult(false, null, message, null, StatusCodes.Status500InternalServerError);
    }

    public static ICommandResult Custom(bool success, object value, string message, IEnumerable<ErrorItem> errors, int statusCode)
    {
        return new CommandResult(success, value, message, errors, statusCode);
    }
    
    public static ICommandResult Custom(bool success, string message, int statusCode)
    {
        return new CommandResult(success, null, message, [], statusCode);
    }
}