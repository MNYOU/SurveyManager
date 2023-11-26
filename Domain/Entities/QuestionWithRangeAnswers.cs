using Domain.Entities.Base;

namespace Domain.Entities;

[Obsolete]
public class QuestionWithRangeAnswers: BaseQuestion
{
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}