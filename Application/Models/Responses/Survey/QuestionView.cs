using Domain.Enums;

namespace Application.Models.Responses.Survey;

public class QuestionView
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public QuestionType Type { get; set; }

    public int Number { get; set; }
    
    public bool IsRequired { get; set; }

    public ICollection<AnswerOptionView> Options { get; set; }
}