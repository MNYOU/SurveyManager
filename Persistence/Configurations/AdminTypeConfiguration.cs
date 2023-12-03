using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AdminTypeConfiguration: IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder
            .HasKey(e => e.Id);
        
        builder
            .HasIndex(e => e.AccessKey)
            .IsUnique();

        // builder
        // .HasNoKey();
        // builder
        // .HasOne(e => e.User)
        // .WithOne()
        // .HasForeignKey<Admin>(e => e.Id);
    }
}