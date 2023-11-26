using Domain.Entities.Base;

namespace Domain.Entities;

[Obsolete]
public class Select: BaseQuestion
{
    public ICollection<AnswerOption> Options { get; set; }
    
    public ICollection<AnswerOption> SelectedOptions { get; set; }
}