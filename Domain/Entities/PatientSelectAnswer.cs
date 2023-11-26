using Domain.Entities.Base;

namespace Domain.Entities;

[Obsolete]
public class PatientSelectAnswer: BasePatientAnswer
{
    public ICollection<AnswerOption> SelectedOptions { get; set; }
}