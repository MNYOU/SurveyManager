using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;

namespace Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder EnableAuditHistory(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuditHistoryEntityTypeConfiguration());
        return modelBuilder;
    }
}