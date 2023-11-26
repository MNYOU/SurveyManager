using Domain.Entities.Base;
using Domain.Enums;

namespace Domain.Entities;

public class Question : Entity
{
    public Guid SurveyId { get; set; }

    public Survey Survey { get; set; }

    public string Title { get; set; }

    public QuestionType Type { get; set; }

    public int SequenceNumber { get; set; }

    public ICollection<AnswerOption>? Options { get; set; }

    public int? MinValue { get; set; }

    public int? MaxValue { get; set; }
}