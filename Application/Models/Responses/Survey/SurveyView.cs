namespace Application.Models.Responses.Survey;

public class SurveyView
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<QuestionView> Questions { get; set; }

    public DateTime CreationTime { get; set; }

    public Guid AdminId { get; set; }
}