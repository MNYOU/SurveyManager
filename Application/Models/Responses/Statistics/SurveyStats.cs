using Application.Models.Responses.Survey;

namespace Application.Models.Responses.Statistics;

public class SurveyStats
{
    public Guid SurveyId { get; set; }
    
    public SurveyPreview Survey { get; set; }

    public ICollection<QuestionStats> Questions { get; set; }
}