using Application.Models.Responses.Admin;
using Domain.Entities;
using Infrastructure.Common.Result;

namespace Application.Services;

public interface IAdminService
{
    public Task<Result> RegisterAdminAsync(User user);

    public Task<Result<AccessKey>> GetAccessCode(Guid adminId);

    public Task<Admin?> CheckAdminAsync(Guid id);

    // public Result ChangeAccessKey { get; set; }
}