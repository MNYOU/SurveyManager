using Domain.Entities.Base;

namespace Domain.Entities;

public class PatientAnswer: Entity
{
    public Guid SurveyAnswerId { get; set; }

    public PatientSurveyAnswer SurveyAnswer { get; set; }
    
    public Guid QuestionId { get; set; }

    public Question Question { get; set; }
    
    public DateTime Date { get; set; }
    
    public ICollection<AnswerOption> SelectedAnswerOptions { get; set; }
    
    public string? TextAnswer { get; set; }

    public int? RangeAnswer { get; set; }
}