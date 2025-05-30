// --------------------------------------------------------------------------------------------------------------------
// file="GenerateTokenCommand.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Contracts.Requests.Identity;

namespace WeatherForecasting.Application.Features.Identity.Commands;

public record GenerateTokenCommand(GenerateTokenRequest Request) : ICommand<ICommandResult>;
