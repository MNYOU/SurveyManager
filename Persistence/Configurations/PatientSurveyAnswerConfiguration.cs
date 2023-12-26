using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PatientSurveyAnswerConfiguration: IEntityTypeConfiguration<PatientSurveyAnswer>
{
    public void Configure(EntityTypeBuilder<PatientSurveyAnswer> builder)
    {
        builder
            .HasOne(e => e.Survey)
            .WithMany()
            .HasForeignKey(e => e.SurveyId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}