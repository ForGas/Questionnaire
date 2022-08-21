namespace Application.Core.Authentification.Commands.Login;

public sealed record class LoginRequest(string Email, string Password);