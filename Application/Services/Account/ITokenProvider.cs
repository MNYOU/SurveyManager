using System.Security.Claims;

namespace Application.Services.Account;

public interface ITokenProvider
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
}