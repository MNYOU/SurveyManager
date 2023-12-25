using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AnalystTypeConfiguration: IEntityTypeConfiguration<Analyst>
{
    public void Configure(EntityTypeBuilder<Analyst> builder)
    {
        builder
            .HasKey(e => e.Id);
        
    }
}