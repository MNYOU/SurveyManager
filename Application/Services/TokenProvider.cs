using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Common.Auth;
using Infrastructure.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenProvider : ITokenProvider
{
    private readonly IConfiguration _config;
    private readonly string _authSectionName;
    private readonly IAuthenticationConfiguration _authConfig;

    public TokenProvider(IConfiguration config, IAuthConfigBuilder confBuilder)
    {
        _authSectionName = "JwtBearer";
        _config = config;
        _authConfig = confBuilder.GetConfiguration();
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.SecurityKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _authConfig.Issuer,
            _authConfig.Audience,
            claims,
            expires: DateTime.Now.Add(_authConfig.Lifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}