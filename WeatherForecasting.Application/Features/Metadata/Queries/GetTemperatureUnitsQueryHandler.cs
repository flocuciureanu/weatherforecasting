// --------------------------------------------------------------------------------------------------------------------
// file="GetTemperatureUnitsQueryHandler.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.ExtensionMethods;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Enums;
using WeatherForecasting.Contracts.Responses.Metadata;

namespace WeatherForecasting.Application.Features.Metadata.Queries;

public class GetTemperatureUnitsQueryHandler : IQueryHandler<GetTemperatureUnitsQuery, ICommandResult>
{
    public Task<ICommandResult> Handle(GetTemperatureUnitsQuery request, CancellationToken cancellationToken)
    {
        var temperatureUnits = Enum.GetValues(typeof(TemperatureUnit))
            .Cast<TemperatureUnit>()
            .Select(e => new MetadataItemResponse(e.ToString(), e.GetDescription()))
            .ToList();
        
        var response = new GetMetadataResponse(temperatureUnits);
        
        return Task.FromResult(CommandResultFactory.Success(response));
    }
}