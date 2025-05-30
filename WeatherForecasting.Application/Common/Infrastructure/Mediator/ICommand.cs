// --------------------------------------------------------------------------------------------------------------------
// file="ICommand.cs">
// --------------------------------------------------------------------------------------------------------------------

using MediatR;

namespace WeatherForecasting.Application.Common.Infrastructure.Mediator;

public interface ICommand<out TResponse> : IRequest<TResponse>;
