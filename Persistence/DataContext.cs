using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Extensions;
using Persistence.Models;

namespace Persistence;

public class DataContext : BaseContext
{
    private readonly IConfiguration _config;

    public DataContext(IConfiguration config)
    {
        _config = config;
    }

    public DataContext(DbContextOptions opt, IConfiguration config) : base(opt)
    {
        _config = config;
    }

    public DbSet<AuditHistory> AuditHistories { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config["ConnectionStrings:Postgresql"]);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("SurveyManager");
        builder.EnableAuditHistory();

        var assembly = GetType().Assembly;
        builder.ApplyConfigurationsFromAssembly(assembly);
    }
}