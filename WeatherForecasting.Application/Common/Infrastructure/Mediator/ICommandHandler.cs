// --------------------------------------------------------------------------------------------------------------------
// file="ICommandHandler.cs">
// --------------------------------------------------------------------------------------------------------------------

using MediatR;

namespace WeatherForecasting.Application.Common.Infrastructure.Mediator;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>;
