// --------------------------------------------------------------------------------------------------------------------
// file="ConfigureSwaggerOptions.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WeatherForecasting.Api.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Weather Forecasting API",
            Description = """
                          This API provides weather data for a given city.

                          ## Endpoints:
                          - **Current Weather**: Returns real-time weather conditions for a specific city.
                          - **Forecast Weather**: Returns 3-hour interval forecast data for up to 5 days in the future for a specific city.

                          ## Access & Rate Limiting:
                          - **Unauthenticated Access**: Limited to **5 requests every 2 minutes**
                          - **Authenticated Access**: Limited to **20 requests every 2 minutes**

                          Authentication is optional but recommended to increase rate limits.

                          ## Authentication:
                          Use a Bearer token in the `Authorization` header to access authenticated limits.
                          
                              Authorization: Bearer {token}

                          """,
        });

        options.EnableAnnotations();
        options.ExampleFilters();
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Standard Authorization header using the Bearer scheme (JWT). Please only add your generated token without the 'bearer' keyword",
            Name = HeaderNames.Authorization,
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme,
        });
        
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    }
}