using Application.Models.Responses.Survey;

namespace Application.Models.Responses.Statistics.Average;

public class SurveyAverageStats
{
    public SurveyPreview Survey { get; set; }

    public ICollection<QuestionAverageStats> Questions { get; set; }
}