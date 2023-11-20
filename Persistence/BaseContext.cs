using DomainServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public abstract class BaseContext: DbContext, IUnitOfWork
{
    protected BaseContext()
    {
        
    }
    
    protected BaseContext(DbContextOptions opt) : base(opt)
    {
        
    }
    
}