// --------------------------------------------------------------------------------------------------------------------
// file="GetCurrentWeatherQueryValidator.cs">
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;
using WeatherForecasting.Application.Common.Constants;

namespace WeatherForecasting.Application.Features.Weather.Queries.GetCurrentWeather;

public class GetCurrentWeatherQueryValidator : AbstractValidator<GetCurrentWeatherQuery>
{
    public GetCurrentWeatherQueryValidator()
    {
        RuleFor(x => x.Request.CityName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100)
            .Matches(@"^[a-zA-Z\s\-']+$")
            .WithMessage(Constants.ErrorMessages.InvalidCityName);
    }
}