// --------------------------------------------------------------------------------------------------------------------
// file="IAuthTokenService.cs">
// --------------------------------------------------------------------------------------------------------------------

namespace WeatherForecasting.Application.Features.Identity.Services;

public interface IAuthTokenService
{
    string GenerateAuthToken(string emailAddress);
}