// --------------------------------------------------------------------------------------------------------------------
// file="ExceptionHandlingMiddleware.cs">
// --------------------------------------------------------------------------------------------------------------------


using System.Globalization;
using WeatherForecasting.Application.Common.Infrastructure.Exceptions.BadRequest;
using WeatherForecasting.Application.Common.Infrastructure.Exceptions.NotFound;
using WeatherForecasting.Application.Common.Infrastructure.Exceptions.TooManyRequests;
using WeatherForecasting.Application.Common.Infrastructure.Exceptions.Unauthorized;
using WeatherForecasting.Application.Common.Infrastructure.Exceptions.Validation;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;

namespace WeatherForecasting.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{EMessage}", e.Message);

            await HandleExceptionAsync(context, e);
        }
    }
    
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var commandResult = GetCommandResult(exception, httpContext);
        
        if (!httpContext.Response.HasStarted)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = commandResult.StatusCode;
        }

        await httpContext.Response.WriteAsJsonAsync(commandResult);
    }

    private static ICommandResult GetCommandResult(Exception exception, HttpContext httpContext) =>
        exception switch
        {
            UnauthorizedException => CommandResultFactory.Unauthorized(exception.Message),
            BadRequestException => CommandResultFactory.Failure(exception.Message),
            NotFoundException => CommandResultFactory.NotFound(exception.Message),
            ValidationException => BuildValidationFailureCommandResult(exception),
            TooManyRequestsException => BuildTooManyRequestsFailureCommandResult(exception, httpContext),
            _ => CommandResultFactory.ServerError(exception.Message)
        };

    private static ICommandResult BuildTooManyRequestsFailureCommandResult(Exception exception, HttpContext httpContext)
    {
        var tooManyRequestsException = exception as TooManyRequestsException;
        
        httpContext.Response.Headers.RetryAfter = ((int)tooManyRequestsException!.RetryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
        
        return CommandResultFactory.Custom(false, exception.Message, StatusCodes.Status429TooManyRequests);
    }

    private static ICommandResult BuildValidationFailureCommandResult(Exception exception)
    {
        var validationException = exception as ValidationException;
        
        return CommandResultFactory.ValidationFailure(validationException?.ValidationErrors);
    }
}