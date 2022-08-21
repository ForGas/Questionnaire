using Application.Common.Services;

namespace Application.Services.Authentification;

public sealed class AuthentificationService : IAuthentificationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthentificationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthentificationResult Register(string email, string password, string firstName, string lastName)
    {
        var userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

        return new AuthentificationResult(
            userId,
            firstName,
            lastName,
            email,
            token
            );
    }

    public AuthentificationResult Login(string email, string password)
    {
        return new AuthentificationResult(
           Guid.NewGuid(),
           "firsName",
           "lastName",
           email,
           "token"
           );
    }
}
