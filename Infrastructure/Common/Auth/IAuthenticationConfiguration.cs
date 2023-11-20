namespace Infrastructure.Common.Auth;

public interface IAuthenticationConfiguration
{
    public string Audience { get; }

    public string Authority { get; }

    public string SecurityKey { get; }

    public string Issuer { get; }

    public TimeSpan Lifetime { get; }
}