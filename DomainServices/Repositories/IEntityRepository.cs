using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Repositories;

public interface IEntityRepository<TEntity>
    where TEntity : Entity
{
    IUnitOfWork UnitOfWork { get; }

    DbSet<TEntity> Items { get; }
    
    // IQueryable<TEntity> Items { get; }

    public Task<List<TEntity>> GetAll();

    public Task<TEntity?> GetById(Guid id);

    public Task<TEntity> AddAsync(TEntity entity);

    public TEntity Update(TEntity entity);

    public void Delete(TEntity entity);
}