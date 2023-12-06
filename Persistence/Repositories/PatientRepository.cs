using Domain.Entities;
using DomainServices.Repositories;

namespace Persistence.Repositories;

public class PatientRepository: EntityRepository<PatientAnswer, DataContext>, IPatientRepository
{
    public PatientRepository(DataContext context) : base(context)
    {
    }
}