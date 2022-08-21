namespace Infrastructure.Authentification;

public sealed class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public const string SecretKeySectionName = "Secret";
    public string Secret { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}
