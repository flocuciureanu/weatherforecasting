// --------------------------------------------------------------------------------------------------------------------
// file="IQuery.cs">
// --------------------------------------------------------------------------------------------------------------------

using MediatR;

namespace WeatherForecasting.Application.Common.Infrastructure.Mediator;

public interface IQuery<out TResponse> : IRequest<TResponse>;
