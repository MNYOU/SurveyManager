using Application.Models.Responses.Survey;

namespace Application.Models.Responses.Statistics.AllAnswers;

public class QuestionStats
{
    public QuestionView Question { get; set; }

    public ICollection<AnswerOptionStats>? AnswerStats { get; set; }

    public TextAnswersStats? TextAnswersStats { get; set; }

    public RangeAllValuesStats? RangeStats { get; set; }
}