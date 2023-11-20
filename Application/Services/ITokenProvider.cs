using System.Security.Claims;

namespace Application.Services;

public interface ITokenProvider
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
}