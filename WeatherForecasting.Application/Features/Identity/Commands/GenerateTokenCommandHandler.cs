// --------------------------------------------------------------------------------------------------------------------
// file="GenerateTokenCommandHandler.cs">
// --------------------------------------------------------------------------------------------------------------------

using WeatherForecasting.Application.Common.Constants;
using WeatherForecasting.Application.Common.Infrastructure.Mediator;
using WeatherForecasting.Application.Features.Identity.Services;
using WeatherForecasting.Contracts.Responses.Identity;

namespace WeatherForecasting.Application.Features.Identity.Commands;

public class GenerateTokenCommandHandler : ICommandHandler<GenerateTokenCommand, ICommandResult>
{
    private readonly IAuthTokenService _authTokenService;

    public GenerateTokenCommandHandler(IAuthTokenService authTokenService)
    {
        _authTokenService = authTokenService;
    }
    
    public Task<ICommandResult> Handle(GenerateTokenCommand command, CancellationToken cancellationToken)
    {
        var token = _authTokenService.GenerateAuthToken(command.Request.EmailAddress);

        var response = new GenerateTokenResponse(token);
        
        return !string.IsNullOrEmpty(token)
            ? Task.FromResult(CommandResultFactory.Success(response))
            : Task.FromResult(CommandResultFactory.Failure(Constants.ErrorMessages.TokenCreationFailure));
    }
}