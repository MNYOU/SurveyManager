using Domain.Entities.Base;

namespace Domain.Entities;

public class AnswerOption: Entity
{
    public Guid? QuestionId { get; set; }

    public Question? Question { get; set; }
    
    public string Answer { get; set; }
}