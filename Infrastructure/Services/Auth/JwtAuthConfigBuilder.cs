using Infrastructure.Common.Auth;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Auth;

public class JwtAuthConfigBuilder : IAuthConfigBuilder
{
    private readonly IConfiguration _configuration;
    private readonly string section;

    public JwtAuthConfigBuilder(IConfiguration configuration)
    {
        _configuration = configuration;
        section = "Authentication:JwtBearer";
    }

    public IAuthenticationConfiguration GetConfiguration()
    {
        return new BaseAuthenticationConfiguration()
        {
            Audience = _configuration[$"{section}:Audience"],
            Authority = _configuration[$"{section}:Authority"],
            Issuer = _configuration[$"{section}:Issuer"],
            SecurityKey = _configuration[$"{section}:SecurityKey"],
            Lifetime = TimeSpan.FromMilliseconds(double.Parse(_configuration[$"{section}:Lifetime"])),
        };
    }
}