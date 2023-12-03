using System.Security.Claims;
using Domain.Enums;

namespace Application.Models.Responses.Account;

public class AuthorizedModel
{
    public Guid Id { get; init; }

    public string Login { get; init; }

    public string Email { get; init; }

    public RolesEnum Role { get; init; }
    // TODO костыль
    public string Token { get; set; }

    public static AuthorizedModel GetByClaimsWithoutToken(ClaimsPrincipal identity)
    {
        return new AuthorizedModel()
        {
            Id = new Guid(identity.Claims.Single(c => c.Type == "Id").Value),
            Email = identity.Claims.Single(c => c.Type == ClaimTypes.Email).Value,
            Login = identity.Claims.Single(c => c.Type == "Login").Value,
            Role =  Enum.Parse<RolesEnum>(identity.Claims.Single(c => c.Type == ClaimTypes.Role).Value)
        };
    }
    
}