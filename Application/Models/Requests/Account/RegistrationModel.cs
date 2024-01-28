using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Infrastructure.Attributes;

namespace Application.Models.Requests.Account;

public record RegistrationModel
{
    [Required] 
    [Login] 
    public string Login { get; init; }

    [Required] 
    [EmailAddress] 
    public string Email { get; init; }

    [Required] 
    public RolesEnum Role { get; init; }

    [Required]
    [Password]
    public string Password { get; init; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Пароли должны совпадать")]
    public string PasswordConfirm { get; init; }
}