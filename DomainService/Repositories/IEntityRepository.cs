using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Repositories;

public interface IEntityRepository<TEntity>
    where TEntity : Entity
{
    IUnitOfWork UnitOfWork { get; }

    DbSet<TEntity> Items { get; }
    // IQueryable<TEntity> Items { get; }
}