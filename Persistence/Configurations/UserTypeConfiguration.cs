using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserTypeConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasIndex(e => e.Login)
            .IsUnique();

        builder
            .HasIndex(e => e.Email)
            .IsUnique();
        
        builder
            .ToTable("users");
    }
}