// --------------------------------------------------------------------------------------------------------------------
// file="TooManyRequestsException.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.Exceptions.TooManyRequests;

public sealed class TooManyRequestsException : Exception
{
    public TooManyRequestsException(string message, TimeSpan retryAfter)
        :base(message)
    {
        RetryAfter = retryAfter;
    }
    
    public TimeSpan RetryAfter { get; set; }
}
