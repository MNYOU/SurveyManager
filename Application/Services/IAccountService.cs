using Application.Common.Result;
using Application.Models.Requests;
using Application.Models.Requests.Account;
using Application.Models.Responses;
using Application.Models.Responses.Account;
using Domain.Entities;
using Infrastructure.Common.Result;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public interface IAccountService<TEntity>
    where TEntity : class
{
    public Task<User?> GetUserById(Guid id);

    public Task<List<User>> GetAll();

    public Task<Result<AuthorizedModel?>> Login(LoginModel request);

    public Task<Result> Register(RegistrationModel request);

    public Task<Result> VerifyEmail(Guid id, string confirmationToken);
}