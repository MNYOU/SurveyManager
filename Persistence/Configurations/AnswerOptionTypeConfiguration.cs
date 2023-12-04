using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AnswerOptionTypeConfiguration: IEntityTypeConfiguration<AnswerOption>
{
    public void Configure(EntityTypeBuilder<AnswerOption> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasOne(e => e.Question)
            .WithMany(e => e.Options)
            .HasForeignKey(e => e.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}