using System.Security.Claims;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

// TODO перейти к IdentityUser
public class User: Entity
{
    // public User()
    // {
        
    // }
    
    // public User(string name): base(name)
    // {
        
    // }
    public Guid Id { get; set; }
    public RolesEnum Role { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public bool EmailIsConfirmed { get; set; }
    public string PasswordHash { get; set; }

    public List<Claim> GetClaims()
    {
        return new List<Claim>()
        {
            new("Id", Id.ToString()),
            new("Login", Login),
            new(ClaimTypes.Email, Email),
            new(ClaimTypes.Role, Role.ToString()),
        };
    }
}