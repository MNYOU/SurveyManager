using System.ComponentModel.DataAnnotations;
using Infrastructure.Attributes;

namespace Application.Models.Requests.Account;

public record LoginModel
{
    [Required]
    [Login]
    public required string Login { get; init; }

    [Required]
    [Password]
    public required string Password { get; init; }
}
