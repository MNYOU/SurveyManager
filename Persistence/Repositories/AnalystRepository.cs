using Domain.Entities;
using DomainServices.Repositories;

namespace Persistence.Repositories;

public class AnalystRepository: EntityRepository<Analyst, DataContext>, IAnalystRepository
{
    public AnalystRepository(DataContext context) : base(context)
    {
    }
}