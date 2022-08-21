using Application.Common.Abstractions;
using Application.Services.Authentification;

namespace Application.Core.Authentification.Commands.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, AuthentificationResponse>
{
    private readonly IAuthentificationService _authentificationService;

    public LoginCommandHandler(IAuthentificationService authentificationService)
    {
        _authentificationService = authentificationService;
    }

    public Task<AuthentificationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var authResult = _authentificationService.Login(
            email: request.Email,
            password: request.Password
            );

        var response = new AuthentificationResponse(
            Id: authResult.Id,
            FirstName: authResult.FirstName,
            LastName: authResult.LastName,
            Email: authResult.Email,
            Token: authResult.Token
            );

        return Task.FromResult(response);
    }
}
