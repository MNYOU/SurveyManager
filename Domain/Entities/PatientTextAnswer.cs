namespace Domain.Entities;

[Obsolete]
public abstract class PatientTextAnswer
{
    public string Answer { get; set; }

    // create entity?
    public string PatientInfo { get; set; }
}