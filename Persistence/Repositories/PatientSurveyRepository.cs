using Domain.Entities;
using DomainServices.Repositories;

namespace Persistence.Repositories;

public class PatientSurveyRepository: EntityRepository<PatientSurveyAnswer, DataContext>, IPatientSurveyRepository
{
    public PatientSurveyRepository(DataContext context) : base(context)
    {
    }
}