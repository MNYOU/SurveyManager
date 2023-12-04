using Application.Models.Requests.Account;
using Application.Models.Responses.Account;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Common.Result;

namespace Application.Services.Account;

public interface IAccountService
{
    public Task<User?> GetUserByIdAsync(Guid id);

    public Task<List<User>> GetAll();

    public Task<Result<AuthorizedModel>> Login(LoginModel request);

    public Task<Result> RegisterAsync(RegistrationModel request);

    public Task<Result> VerifyEmailAsync(Guid id, string confirmationToken);

    public Task<bool> CheckUserInRoleAsync(Guid userId, RolesEnum role);
}