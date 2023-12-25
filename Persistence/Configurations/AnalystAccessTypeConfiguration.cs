using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AnalystAccessTypeConfiguration: IEntityTypeConfiguration<AnalystAccess>
{
    public void Configure(EntityTypeBuilder<AnalystAccess> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasOne(e => e.Analyst)
            .WithMany(e => e.Accesses)
            .HasForeignKey(e => e.AnalystId);
        
    }
}