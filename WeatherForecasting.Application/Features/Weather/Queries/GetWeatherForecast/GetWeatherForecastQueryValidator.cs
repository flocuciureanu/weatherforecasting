// --------------------------------------------------------------------------------------------------------------------
// file="GetWeatherForecastQueryValidator.cs">
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;
using WeatherForecasting.Application.Common.Constants;

namespace WeatherForecasting.Application.Features.Weather.Queries.GetWeatherForecast;

public class GetWeatherForecastQueryValidator : AbstractValidator<GetWeatherForecastQuery>
{
    public GetWeatherForecastQueryValidator()
    {
        RuleFor(x => x.Request.CityName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100)
            .Matches(@"^[a-zA-Z\s\-']+$")
            .WithMessage(Constants.ErrorMessages.InvalidCityName);
        
        RuleFor(x => x.Request.ForecastDate)
            .Must(CheckForecastDateIsValid)
            .WithMessage(Constants.ErrorMessages.InvalidForecastDate);
    }

    private static bool CheckForecastDateIsValid(DateOnly forecastDate)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var maxDate = today.AddDays(5);
    
        return forecastDate >= today && forecastDate <= maxDate;
    }
}