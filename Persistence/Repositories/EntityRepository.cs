using Domain.Entities;
using Domain.Entities.Base;
using DomainServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public abstract class EntityRepository<TEntity, TDbContext> : IEntityRepository<TEntity> 
    where TEntity : Entity
    where TDbContext : BaseContext, IUnitOfWork
{
    protected EntityRepository(TDbContext context)
    {
        UnitOfWork = context;
        Items = context.Set<TEntity>();
    }

    public IUnitOfWork UnitOfWork { get; }
    public DbSet<TEntity> Items { get; }

    public virtual async Task<List<TEntity>> GetAll()
    {
        return await Items.ToListAsync();
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
        return await Items.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        return (await Items.AddAsync(entity)).Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        var result = Items.Update(entity);
        return result.Entity;
    }

    public virtual void Delete(TEntity entity)
    {
        var result = Items.Remove(entity);
    }
}