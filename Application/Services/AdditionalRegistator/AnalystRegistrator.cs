using Domain.Entities;
using Domain.Enums;
using DomainServices.Repositories;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.Extensions.Logging;

namespace Application.Services.AdditionalRegistator;

public class AnalystRegistrator: IAdditionalRegistrator
{
    private readonly IAnalystRepository _repository;
    private readonly ICustomLogger _logger;

    public RolesEnum Role => RolesEnum.Analyst;

    public AnalystRegistrator(IAnalystRepository repository, ICustomLogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result> Register(User user)
    {
        var analyst = await CheckAnalystAsync(user.Id);
        if (analyst != null) return Result.Error("Аналитик с такими данными уже зарегистрирован.");
        analyst = new Analyst
        {
            Id = user.Id,
        };
        await _repository.AddAsync(analyst);
        try
        {
            await _repository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.Error("Ошибка при регистрации аналитика", e.Message);
        }

        return Result.Success();
    }

    public async Task<Result> Delete(User user)
    {
        var analyst = await CheckAnalystAsync(user.Id);
        if (analyst is null) 
            return Result.Error("Администратор с такими данными уже зарегистрирован.");
        try
        {
            _repository.Items.Remove(analyst);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error ,"Ошибка при удалени админа. " + e.Message);
            return Result.Error("Ошибка при удалени админа.");
        }

        return Result.Success();
    }

    private async Task<Analyst?> CheckAnalystAsync(Guid id)
    {
        // var user = await _accountService.GetUserByIdAsync(id);
        // if (user == null) return null;
        return await _repository.GetById(id);
    }
}