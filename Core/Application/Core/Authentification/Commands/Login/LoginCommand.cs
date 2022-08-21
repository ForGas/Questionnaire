using Application.Common.Abstractions;

namespace Application.Core.Authentification.Commands.Login;

public sealed record class LoginCommand(string Email, string Password) : ICommand<AuthentificationResponse>;
