using Domain.Entities.Base;

namespace Domain.Entities;

public class Survey: Entity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public bool ContainsDefaultQuestions { get; set; } = true;
    public ICollection<Question> Questions { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid AdminId { get; set; }

    public Admin Admin { get; set; }
    
    // public string Code { get; set; }
}