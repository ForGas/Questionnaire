namespace Application.Core.Authentification.Commands;

public record class AuthentificationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
    );
