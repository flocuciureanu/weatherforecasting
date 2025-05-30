// --------------------------------------------------------------------------------------------------------------------
// file="CustomAuthorizationMiddleware.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;

namespace WeatherForecasting.Api.Middlewares;

public class CustomAuthorizationMiddleware : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Challenged || authorizeResult.Forbidden)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var result = CommandResultFactory.Unauthorized("You are not authorized to access this resource.");
            await context.Response.WriteAsJsonAsync(result);
            return;
        }

        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}