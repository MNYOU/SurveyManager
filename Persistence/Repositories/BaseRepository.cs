using Domain.Entities;
using DomainServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity, TDbContext> : IEntityRepository<TEntity> 
    where TEntity : Entity
    where TDbContext : BaseContext, IUnitOfWork
{
    protected BaseRepository(TDbContext context)
    {
        UnitOfWork = context;
        Items = context.Set<TEntity>();
    }

    public IUnitOfWork UnitOfWork { get; }
    public DbSet<TEntity> Items { get; }
}