using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class QuestionTypeConfiguration: IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasOne(e => e.Survey)
            .WithMany(e => e.Questions)
            .HasForeignKey(e => e.SurveyId);

        builder
            .Property(e => e.IsRequired)
            .HasDefaultValue(true);
    }
}