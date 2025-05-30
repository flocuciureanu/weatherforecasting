// --------------------------------------------------------------------------------------------------------------------
// file="BadRequestException.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.Exceptions.BadRequest;

public sealed class BadRequestException(string message) : Exception(message);
