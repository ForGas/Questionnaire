namespace Application.Core.Authentification.Commands.Register;

public record class RegisterRequest(string FirstName, string LastName, string Email, string Password);
