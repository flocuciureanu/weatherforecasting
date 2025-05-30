// --------------------------------------------------------------------------------------------------------------------
// file="GenerateTokenCommandValidator.cs">
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;
using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.ExtensionMethods;

namespace WeatherForecasting.Application.Features.Identity.Commands;

public class GenerateTokenCommandValidator : AbstractValidator<GenerateTokenCommand>
{
    public GenerateTokenCommandValidator()
    {
        RuleFor(x => x.Request.EmailAddress)
            .Must(x => x.IsValidEmailAddress()).WithMessage(Constants.ErrorMessages.InvalidEmailAddress);
    }
}