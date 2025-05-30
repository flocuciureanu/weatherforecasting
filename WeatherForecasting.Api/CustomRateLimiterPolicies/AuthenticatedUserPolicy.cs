// --------------------------------------------------------------------------------------------------------------------
// file="AuthenticatedUserPolicy.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Exceptions.TooManyRequests;

namespace WeatherForecasting.Api.CustomRateLimiterPolicies;

internal class AuthenticatedUserPolicy : IRateLimiterPolicy<string>
{
    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        const int nonAuthenticatedPermitLimit = 5; // 5 requests per 2 minutes
        const int authenticatedPermitLimit = 20; // 20 requests per 2 minutes
        var window = TimeSpan.FromMinutes(2);
 
        var isAuthenticated = httpContext.User.Identity?.IsAuthenticated == true;
 
        if (isAuthenticated)
        {
            var emailAddress = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            
            return RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: emailAddress!,
                _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = authenticatedPermitLimit,
                    Window = window
                }
            );
        }
 
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress!.ToString(),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = nonAuthenticatedPermitLimit,
                Window = window
            }
        );
    }

    public Func<OnRejectedContext, CancellationToken, ValueTask> OnRejected =>
        (context, _) =>
        {
            context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter);

            throw new TooManyRequestsException(Constants.ErrorMessages.TooManyRequestsError, retryAfter);
        };
}