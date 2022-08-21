using Application.Common.Abstractions;
using Application.Services.Authentification;

namespace Application.Core.Authentification.Commands.Register;

public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthentificationResponse>
{
    private readonly IAuthentificationService _authentificationService;

    public RegisterCommandHandler(IAuthentificationService authentificationService)
    {
        _authentificationService = authentificationService;
    }

    public Task<AuthentificationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var authResult = _authentificationService.Register(
            email: request.Email,
            password: request.Password,
            firstName: request.FirstName,
            lastName: request.LastName
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
