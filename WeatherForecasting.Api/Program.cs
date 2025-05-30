using System.Reflection;
using System.Text.Json.Serialization;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using WeatherForecasting.Api.CustomRateLimiterPolicies;
using WeatherForecasting.Api.Health;
using WeatherForecasting.Api.Middlewares;
using WeatherForecasting.Api.Swagger;
using WeatherForecasting.Application;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddAuthorization();
builder.Services.AddHealthChecks()
    .AddCheck<OpenWeatherMapHealthCheck>("OpenWeatherMap");
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddMemoryCache();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy(RateLimiterPolicies.AuthenticatedUserPolicy, new AuthenticatedUserPolicy());
});
builder.Services.AddApplication();
builder.Services.AddCommandValidation();
builder.Services.AddAuth(config);
builder.Services.AddAppSettings(config);
builder.Services.AddWeatherHttpClient();
builder.Services.AddMediator();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddleware>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather Forecasting API v1");
    c.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();
app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();

app.Run();
