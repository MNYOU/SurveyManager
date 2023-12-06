using Domain.Entities.Base;

namespace Domain.Entities;

public class PatientSurveyAnswer: Entity
{
    public Guid? SurveyId { get; set; }

    public Survey? Survey { get; set; }
}