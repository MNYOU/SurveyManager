using Domain.Enums;

namespace Application.Models.Responses.Super;

public class UserView
{
    public Guid Id { get; set; }
    public RolesEnum Role { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public bool EmailIsConfirmed { get; set; }
}