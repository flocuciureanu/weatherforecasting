// --------------------------------------------------------------------------------------------------------------------
// file="JwtBearerSettings.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.AppSettings.Authentication;

public class JwtBearerSettings
{
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public int LifetimeInMinutes { get; init; }
}