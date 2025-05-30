// --------------------------------------------------------------------------------------------------------------------
// file="IQueryHandler.cs">
// --------------------------------------------------------------------------------------------------------------------

using MediatR;

namespace WeatherForecasting.Application.Common.Infrastructure.Mediator;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;
