using Application.Models.Responses.Survey;

namespace Application.Models.Responses.Statistics;

public class AnswerOptionStats
{
    public Guid AnswerOptionId { get; set; }
    
    public AnswerOptionView Answer { get; set; }

    public int Count { get; set; }

    public int Percent { get; set; }
}