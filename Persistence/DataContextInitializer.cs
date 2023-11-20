using Infrastructure.Services.DbInitializer;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContextInitializer: IDbContextInitializer
{
    private readonly DataContext _context;
    public DataContextInitializer(DataContext context)
    {
        _context = context;
    }
    
    public async Task Migrate()
    {
        await _context.Database.MigrateAsync();
    }

    public Task Seed()
    {
        return Task.CompletedTask;
        // throw new NotImplementedException();
    }
}