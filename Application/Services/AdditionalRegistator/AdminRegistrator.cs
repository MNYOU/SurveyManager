using Domain.Entities;
using Domain.Enums;
using DomainServices.Repositories;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.AdditionalRegistator;

public class AdminRegistrator: IAdditionalRegistrator
{
    private readonly IAdminRepository _repository;
    private readonly ICustomLogger _logger;

    public AdminRegistrator(IAdminRepository repository, ICustomLogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public RolesEnum Role => RolesEnum.Admin;
    
    public async Task<Result> Register(User user)
    {
        var admin = await CheckAdminAsync(user.Id);
        if (admin != null) 
            return Result.Error("Администратор с такими данными уже зарегистрирован.");
        
        admin = new Admin 
        {
            Id = user.Id,
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

    public async Task<Result> Delete(User user)
    {
        var admin = await CheckAdminAsync(user.Id);
        if (admin is null) 
            return Result.Error("Администратор с такими данными уже зарегистрирован.");
        try
        {
            _repository.Items.Remove(admin);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error ,"Ошибка при удалени админа. " + e.Message);
            return Result.Error("Ошибка при удалени админа.");
        }

        return Result.Success();
    }

    private async Task<Admin?> CheckAdminAsync(Guid id)
    {
        // var user = await _accountService.GetUserByIdAsync(id);
        // if (user == null) return null;
        var admin = await _repository.GetById(id);
        return admin;
    }
}