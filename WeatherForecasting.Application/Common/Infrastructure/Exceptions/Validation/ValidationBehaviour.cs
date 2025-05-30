// --------------------------------------------------------------------------------------------------------------------
// file="ValidationBehaviour.cs">
// --------------------------------------------------------------------------------------------------------------------

using FluentValidation;
using MediatR;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;

namespace WeatherForecasting.Application.Common.Infrastructure.Exceptions.Validation;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count <= 0) 
            return await next(cancellationToken);

        var errorItems = failures
            .Select(f => new ErrorItem
            {
                PropertyName = f.PropertyName,
                Message = f.ErrorMessage
            });
        
        throw new ValidationException(errorItems);
    }
}