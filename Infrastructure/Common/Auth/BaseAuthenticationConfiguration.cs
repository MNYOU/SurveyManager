using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common.Auth;

public class BaseAuthenticationConfiguration: IAuthenticationConfiguration
{
    public string Audience { get; init; }
    public string Authority { get; init; }
    public string SecurityKey { get; init; }
    public string Issuer { get; init; }
    public TimeSpan Lifetime { get; init; }
}