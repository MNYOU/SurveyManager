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
            .Property(e => e.ContainsDefaultQuestions)
            .HasDefaultValue(true);
        
        builder
            .HasOne(e => e.Admin)
            .WithMany(e => e.Surveys)
            .HasForeignKey(e => e.AdminId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}