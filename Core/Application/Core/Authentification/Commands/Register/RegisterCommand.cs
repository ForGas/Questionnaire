using Application.Common.Abstractions;

namespace Application.Core.Authentification.Commands.Register;

public sealed record class RegisterCommand(string FirstName, string LastName, string Email, string Password) 
    : ICommand<AuthentificationResponse>;
