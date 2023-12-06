using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PatientAnswerTypeConfiguration : IEntityTypeConfiguration<PatientAnswer>
{
    public void Configure(EntityTypeBuilder<PatientAnswer> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasOne(e => e.Question)
            .WithMany()
            .HasForeignKey(e => e.QuestionId);

        builder
            .HasMany(e => e.SelectedAnswerOptions)
            .WithMany();

        builder
            .HasOne(e => e.SurveyAnswer)
            .WithMany()
            .HasForeignKey(e => e.SurveyAnswerId);
    }
}