using Application.Models.Responses.Survey;

namespace Application.Models.Responses.Statistics;

public class QuestionStats
{
    public Guid QuestionId { get; set; }

    public QuestionView Question { get; set; }

    public ICollection<AnswerOptionStats> AnswerStats { get; set; }
}