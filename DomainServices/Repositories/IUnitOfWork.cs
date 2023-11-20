namespace DomainServices.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    int SaveChanges();

    int SaveChanges(bool acceptAllChangesOnSuccess);
}