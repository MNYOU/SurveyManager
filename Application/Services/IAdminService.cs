using Application.Models.Responses.Admin;
using Infrastructure.Common.Result;

namespace Application.Services;

public interface IAdminService
{
    public Task<Result<AccessKey>> GetAccessCode(Guid adminId);
}