// --------------------------------------------------------------------------------------------------------------------
// file="ValidationException.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Infrastructure.Mediator;

namespace WeatherForecasting.Application.Common.Infrastructure.Exceptions.Validation;

public sealed class ValidationException(IEnumerable<ErrorItem> validationErrors)
    : FluentValidation.ValidationException(Constants.Constants.ErrorMessages.ValidationFailureMessage)
{
    public IEnumerable<ErrorItem> ValidationErrors { get; } = validationErrors;
}