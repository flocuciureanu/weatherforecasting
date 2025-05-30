// --------------------------------------------------------------------------------------------------------------------
// file="AuthenticationSettings.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Common.AppSettings.Authentication;

public class AuthenticationSettings : IAppSettings
{
    public JwtBearerSettings JwtBearerSettings { get; init; }
}