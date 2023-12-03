using Domain.Entities;
using DomainServices.Repositories;

namespace Persistence.Repositories;

public class AdminRepository: EntityRepository<Admin, DataContext>, IAdminRepository
{
    public AdminRepository(DataContext context) : base(context)
    {
    }
}