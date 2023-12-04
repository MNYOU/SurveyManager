using Domain.Entities.Base;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities;

public class Question : Entity
{
    public Guid? SurveyId { get; set; }
    
    public Survey? Survey { get; set; }

    public string Title { get; set; }

    public QuestionType Type { get; set; }

    public bool IsRequired { get; set; }

    public bool IsDefault { get; set; }

    public int Number { get; set; }

    public ICollection<AnswerOption> Options { get; set; }

    public int? MinValue { get; set; }

    public int? MaxValue { get; set; }
}