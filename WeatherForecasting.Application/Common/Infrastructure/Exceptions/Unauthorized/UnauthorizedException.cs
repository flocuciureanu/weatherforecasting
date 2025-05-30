// --------------------------------------------------------------------------------------------------------------------
// file="UnauthorizedException.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.Exceptions.Unauthorized;

public sealed class UnauthorizedException(string message = Constants.Constants.ErrorMessages.Unauthorized) : Exception(message);
