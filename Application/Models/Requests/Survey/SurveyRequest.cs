namespace Application.Models.Requests.Survey;

public class SurveyRequest
{
    public Guid Id { get; set; }

    public ICollection<QuestionRequest> Questions { get; set; }
}