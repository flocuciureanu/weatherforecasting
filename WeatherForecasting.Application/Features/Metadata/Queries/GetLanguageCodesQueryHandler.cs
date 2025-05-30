// --------------------------------------------------------------------------------------------------------------------
// file="GetLanguageCodesQueryHandler.cs">
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Options;
using WeatherForecasting.Application.Common.AppSettings.OpenWeatherMap;
using WeatherForecasting.Application.Common.ExtensionMethods;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Responses.Metadata;

namespace WeatherForecasting.Application.Features.Metadata.Queries;

public class GetLanguageCodesQueryHandler : IQueryHandler<GetLanguageCodesQuery, ICommandResult>
{
    private readonly OpenWeatherMapSettings _openWeatherMapSettings;

    public GetLanguageCodesQueryHandler(IOptions<OpenWeatherMapSettings> options)
    {
        _openWeatherMapSettings = options.Value;
    }
    
    public Task<ICommandResult> Handle(GetLanguageCodesQuery request, CancellationToken cancellationToken)
    {
        var supportedLanguages = _openWeatherMapSettings.SupportedLanguages;

        var languages = supportedLanguages
            .Select(code => new { code, success = CultureInfoExtensions.TryGetCultureInfo(code, out var culture), culture })
            .Where(x => x.success)
            .Select(x => new MetadataItemResponse(x.code, x.culture!.EnglishName))
            .ToList();

        var response = new GetMetadataResponse(languages);

        return Task.FromResult(CommandResultFactory.Success(response));
    }
}