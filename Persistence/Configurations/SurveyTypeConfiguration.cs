using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SurveyTypeConfiguration: IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasOne(e => e.Admin)
            .WithMany()
            .HasForeignKey(e => e.AdminId);
        
    }
}