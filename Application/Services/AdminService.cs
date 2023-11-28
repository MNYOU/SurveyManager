using Application.Models.Responses.Admin;
using Infrastructure.Common.Result;

namespace Application.Services;

public class AdminService: IAdminService
{
    public async Task<Result<AccessKey>> GetAccessCode(Guid adminId)
    {
        throw new NotImplementedException();
    }
}