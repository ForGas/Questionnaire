namespace Application.Services.Authentification;

public interface IAuthentificationService
{
    AuthentificationResult Register(string email, string password, string firstName, string lastName);
    AuthentificationResult Login(string email, string password);
}