namespace Infrastructure.Services.DbInitializer;

public interface IDbContextInitializer
{
    Task Migrate();
    Task Seed();
}