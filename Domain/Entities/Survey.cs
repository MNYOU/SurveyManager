using Domain.Entities.Base;

namespace Domain.Entities;

public class Survey: Entity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    // public ICollection<Select> Selects { get; set; }

    public ICollection<Question> Questions { get; set; }

    // public ICollection<QuestionWithRangeAnswers> QuestionsWithRangeAnswers { get; set; }
    
    // public ICollection<QuestionWithTextAnswer> QuestionsWithTextAnswer { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid AdminId { get; set; }

    public User Admin { get; set; }
    
    // public string Code { get; set; }
}