// --------------------------------------------------------------------------------------------------------------------
// file="NotFoundException.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.Infrastructure.Exceptions.NotFound;

public sealed class NotFoundException(string message) : Exception(message);