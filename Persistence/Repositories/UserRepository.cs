using Domain.Entities;
using DomainServices.Repositories;

namespace Persistence.Repositories;

public class UserRepository: BaseRepository<User, DataContext>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}