using Application.Models.Responses.Admin;
using Application.Services.Account;
using Domain.Entities;
using DomainServices.Repositories;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class AdminService: IAdminService
{
    private readonly IAdminRepository _repository;
    private readonly IAccountService _accountService;
    private readonly ICustomLogger _logger;
    
    public AdminService(IAdminRepository repository, IAccountService accountService, ICustomLogger logger)
    {
        _repository = repository;
        _accountService = accountService;
        _logger = logger;
    }

    public async Task<Admin?> CheckAdminAsync(Guid id)
    {
        var user = await _accountService.GetUserByIdAsync(id);
        if (user == null) return null;
        var admin = await _repository.GetById(id);
        return admin;
    }

    public async Task<Result> ChangeAccessKey(Guid id)
    {
        var user = await _accountService.GetUserByIdAsync(id);
        throw new NotImplementedException();
    }

    public async Task<Admin?> FindByAccessKey(Guid key)
    {
        return await _repository.Items
            .Include(e => e.Surveys)
            .FirstOrDefaultAsync(e => e.AccessKey == key);
    }

    [Obsolete]
    public async Task<Result> RegisterAdminAsync(User user)
    {
        var admin = await CheckAdminAsync(user.Id);
        if (admin != null) return Result.Error("Администратор с такими данными уже зарегистрирован.");
        admin = new Admin 
        {
            AccessKey = Guid.NewGuid(),
        };
        await _repository.AddAsync(admin);
        try
        {
            await _repository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.Error("Ошибка при регистрации администратора", e.Message);
        }

        return Result.Success();
    }

    public async Task<Result<AccessKey>> GetAccessCode(Guid adminId)
    {
        var admin = await CheckAdminAsync(adminId);
        if (admin == null) return Result.Error("Администратор не найден.");

        return Result.Success(new AccessKey(adminId, admin.AccessKey));
    }
}