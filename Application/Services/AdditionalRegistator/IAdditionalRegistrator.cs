using Domain.Entities;
using Domain.Enums;
using Infrastructure.Common.Result;

namespace Application.Services.AdditionalRegistator;

// TODO регистировать для ролей
public interface IAdditionalRegistrator
{
    public RolesEnum Role { get; }

    public Task<Result> Register(User user);
    
    public Task<Result> Delete(User user);
}