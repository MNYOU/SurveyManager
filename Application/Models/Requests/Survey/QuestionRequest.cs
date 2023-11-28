using Application.Models.Responses.Survey;

namespace Application.Models.Requests.Survey;

public class QuestionRequest
{
    public Guid Id { get; set; }

    public string? TextAnswer { get; set; }

    public int? RangeValue { get; set; }

    public ICollection<AnswerOptionRequest>? SelectedOptions { get; set; }
}