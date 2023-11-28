using Domain.Enums;

namespace Application.Models.Requests.Survey;

public class CreateQuestionRequest
{
    public string Title { get; set; }

    public QuestionType Type { get; set; }

    public int Number { get; set; }
    
    public bool IsRequired { get; set; }

    public ICollection<CreateAnswerOptionRequest> AnswerOptions { get; set; }
}