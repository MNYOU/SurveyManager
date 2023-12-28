namespace Application.Models.Responses.Statistics;

public class TextAnswersStats
{
    public int Count { get; set; }
    
    public List<string> Answers { get; set; } = new();
}