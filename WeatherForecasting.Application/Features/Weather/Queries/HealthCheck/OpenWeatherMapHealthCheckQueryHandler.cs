// --------------------------------------------------------------------------------------------------------------------
// file="OpenWeatherMapHealthCheckQueryHandler.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Application.ExternalServices.OpenWeatherMap.Services.HealthCheck;

namespace WeatherForecasting.Application.Features.Weather.Queries.HealthCheck;

public class OpenWeatherMapHealthCheckQueryHandler : IQueryHandler<OpenWeatherMapHealthCheckQuery, ICommandResult>
{
    private readonly IOpenWeatherMapHealthCheckService _healthCheckService;

    public OpenWeatherMapHealthCheckQueryHandler(IOpenWeatherMapHealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    public async Task<ICommandResult> Handle(OpenWeatherMapHealthCheckQuery request, CancellationToken cancellationToken)
    {
        await _healthCheckService.HealthCheckAsync(cancellationToken);

        return CommandResultFactory.Success(true);
    }
}