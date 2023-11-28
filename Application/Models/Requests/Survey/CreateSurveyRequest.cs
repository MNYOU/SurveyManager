namespace Application.Models.Requests.Survey;

public record CreateSurveyRequest
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<CreateQuestionRequest> Questions { get; set; }
}