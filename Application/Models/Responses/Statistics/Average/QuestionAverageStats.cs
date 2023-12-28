using Application.Models.Responses.Statistics.AllAnswers;
using Application.Models.Responses.Survey;

namespace Application.Models.Responses.Statistics.Average;

public class QuestionAverageStats
{
    public QuestionView Question { get; set; }

    public AnswerOptionStats? OptionStats { get; set; }
    
    public RangeStats? AverageRange { get; set; }

    public TextAnswersStats TextAnswersStats { get; set; } = new();
}