using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Repositories;

public interface IUserRepository: IEntityRepository<User>
{
}