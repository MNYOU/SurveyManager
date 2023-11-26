using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Models;

namespace Persistence;

// TODO AuditHistory
// cqrs. было бы удобно использовать для опросов
public class DataContext : BaseContext
{
    private readonly IConfiguration _config;

    public DataContext(DbContextOptions opt, IConfiguration config) : base(opt)
    {
        _config = config;
    }

    public DbSet<AuditHistory> AuditHistories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    // public DbSet<BaseQuestion> Questions { get; set; }
    // public DbSet<BasePatientAnswer> PatientAnswers { get; set; }
    // public DbSet<AnswerOption> AnswerOptions { get; set; }

    public DbSet<Question> Questions { get; set; }
    // public DbSet<QuestionWithTextAnswer> QuestionsWithText { get; set; }
    // public DbSet<QuestionWithRangeAnswers> QuestionsWithRange { get; set; }
    // public DbSet<Select> Selects { get; set; }

    public DbSet<PatientAnswer> PatientAnswers { get; set; }
    // public DbSet<PatientTextAnswer> TextAnswers { get; set; }
    // public DbSet<PatientRangeAnswer> RangeAnswers { get; set; }
    // public DbSet<PatientSelectAnswer> SelectAnswers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_config["ConnectionStrings:Postgresql"]);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("SurveyManager");
        // builder.EnableAuditHistory();

        var assembly = GetType().Assembly;
        // builder.ApplyConfiguration(new AuditHistoryEntityTypeConfiguration());
        builder.ApplyConfigurationsFromAssembly(assembly);
    }
}