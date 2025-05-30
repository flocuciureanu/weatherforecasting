// --------------------------------------------------------------------------------------------------------------------
// file="OpenWeatherMapHealthCheck.cs">
// --------------------------------------------------------------------------------------------------------------------

using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WeatherForecasting.Api.Health;

public class OpenWeatherMapHealthCheck : IHealthCheck
{
    private readonly IMediator _mediator;

    public OpenWeatherMapHealthCheck(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        try
        {
            await _mediator.Send(new OpenWeatherMapHealthCheckQuery(), cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy(exception: e);
        }
    }
}