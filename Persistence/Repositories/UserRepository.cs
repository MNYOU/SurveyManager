using Domain.Entities;
using DomainServices.Repositories;

namespace Persistence.Repositories;

public class UserRepository: EntityRepository<User, DataContext>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}