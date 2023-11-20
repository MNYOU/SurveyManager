using Domain.Enums;

namespace Application.Models.Responses.Account;

public record AuthorizedModel
{
    public Guid Id { get; init; }

    public string Login { get; init; }

    public string Email { get; init; }

    public RolesEnum Role { get; init; }
    // TODO костыль
    public string Token { get; set; }
}