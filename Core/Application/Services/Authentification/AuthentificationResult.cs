namespace Application.Services.Authentification;

public sealed record class AuthentificationResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
    );