using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.Configurations;

public class AuditHistoryEntityTypeConfiguration: IEntityTypeConfiguration<AuditHistory>
{
    public void Configure(EntityTypeBuilder<AuditHistory> builder)
    {
        builder.Property(c => c.Id)
            .UseIdentityColumn(); //TODO: Possibly change this to avoid integer overflow, or cleanup every once in a while
        builder.Property(c => c.RowId)
            .IsRequired()
            .HasMaxLength(128);
        builder.Property(c => c.TableName)
            .IsRequired()
            .HasMaxLength(128);
        builder.Property(c => c.Changed);
        builder.Property(c => c.Username)
            .HasMaxLength(128);
        // This MSSQL only
        //b.Property(c => c.Created).HasDefaultValueSql("getdate()");
        builder.Ignore(t => t.AutoHistoryDetails);
        builder.ToTable("AuditHistory");
    }
}