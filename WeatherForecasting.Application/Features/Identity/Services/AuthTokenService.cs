// --------------------------------------------------------------------------------------------------------------------
// file="AuthTokenService.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WeatherForecasting.Application.Common.AppSettings.Authentication;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WeatherForecasting.Application.Features.Identity.Services;

public class AuthTokenService : IAuthTokenService
{
    private readonly AuthenticationSettings _authenticationSettings;

    public AuthTokenService(IOptions<AuthenticationSettings> authenticationSettings)
    {
        _authenticationSettings = authenticationSettings.Value;
    }

    public string GenerateAuthToken(string emailAddress)
    {
        var tokenSecret = _authenticationSettings.JwtBearerSettings.Key;
        var lifetime = _authenticationSettings.JwtBearerSettings.LifetimeInMinutes;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(tokenSecret!);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, emailAddress),
            new(JwtRegisteredClaimNames.Email, emailAddress)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(lifetime),
            Issuer = _authenticationSettings.JwtBearerSettings.Issuer,
            Audience = _authenticationSettings.JwtBearerSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        var jwtToken = tokenHandler.WriteToken(token);
        
        return jwtToken;
    }
}