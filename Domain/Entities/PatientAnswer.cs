using Domain.Entities.Base;

namespace Domain.Entities;

public class PatientAnswer: Entity
{
    public Guid QuestionId { get; set; }

    public Question Question { get; set; }
    
    public DateTime Date { get; set; }

    public string? PatientFIO { get; set; }
    
    public ICollection<AnswerOption> SelectedAnswerOptions { get; set; }
    
    public string? TextAnswer { get; set; }

    public int? RangeAnswer { get; set; }
}