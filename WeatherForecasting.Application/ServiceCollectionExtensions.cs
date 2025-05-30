// --------------------------------------------------------------------------------------------------------------------
// file="ServiceCollectionExtensions.cs">
// --------------------------------------------------------------------------------------------------------------------

using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.IdentityModel.Tokens;
using Polly;
using WeatherForecasting.Application.Common.AppSettings.Authentication;
using WeatherForecasting.Application.Common.AppSettings.OpenWeatherMap;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Caching.MemoryCaching;
using WeatherForecasting.Application.Common.Infrastructure.Exceptions.Validation;
using WeatherForecasting.Application.Common.Infrastructure.HttpClients;
using WeatherForecasting.Application.Common.Infrastructure.Serialization;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Engine;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetCurrentWeatherData;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.GetWeatherForecastData;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.HealthCheck;
using WeatherForecasting.Application.Features.Identity.Services;

namespace WeatherForecasting.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient(typeof(IOpenWeatherMapEngine<,>), typeof(OpenWeatherMapEngine<,>));
        services.AddTransient<IGetCurrentWeatherDataService, GetCurrentWeatherDataService>();
        services.AddTransient<IGetWeatherForecastDataService, GetWeatherForecastDataService>();
        services.AddTransient<IOpenWeatherMapHealthCheckService, OpenWeatherMapHealthCheckService>();
        services.AddTransient<IAppJsonSerializer, AppJsonSerializer>();
        services.AddTransient<IAuthTokenService, AuthTokenService>();
        services.AddTransient<IAppCache, AppCache>();

        return services;
    }

    public static IServiceCollection AddCommandValidation(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssemblyContaining<IApplicationAssemblyMarker>(ServiceLifetime.Singleton);
        
        return services;
    }
    
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationSettings = new AuthenticationSettings();
        configuration.GetSection(nameof(AuthenticationSettings)).Bind(authenticationSettings);

        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtBearerSettings.Key)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidIssuer = authenticationSettings.JwtBearerSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = authenticationSettings.JwtBearerSettings.Audience,
            });
        
        return services;
    }
    
    public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OpenWeatherMapSettings>(configuration.GetSection(nameof(OpenWeatherMapSettings)));
        services.Configure<AuthenticationSettings>(configuration.GetSection(nameof(AuthenticationSettings)));
        
        return services;
    }

    public static IServiceCollection AddWeatherHttpClient(this IServiceCollection services)
    {
        var shouldHandle = new PredicateBuilder<HttpResponseMessage>()
            .Handle<HttpRequestException>()
            .HandleResult(response => !response.IsSuccessStatusCode);
        
        services.AddHttpClient<WeatherHttpClient>()
            .AddResilienceHandler(Constants.HttClientOptions.ResiliencePipelineName, resilienceBuilder =>
            {
                resilienceBuilder.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 4,
                    Delay = TimeSpan.FromSeconds(2),
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    ShouldHandle = shouldHandle

                });
                resilienceBuilder.AddTimeout(TimeSpan.FromSeconds(3));
                resilienceBuilder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
                {
                    SamplingDuration = TimeSpan.FromSeconds(10),
                    FailureRatio = 0.2,
                    MinimumThroughput = 3,
                    BreakDuration = TimeSpan.FromSeconds(1),
                    ShouldHandle = shouldHandle
                });
            });

        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        var assembly = typeof(IApplicationAssemblyMarker).Assembly;

        services.AddMediatR(x => x.RegisterServicesFromAssembly(assembly));
        
        return services;
    }
}