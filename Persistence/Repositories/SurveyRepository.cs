using Domain.Entities;
using DomainServices.Repositories;

namespace Persistence.Repositories;

public class SurveyRepository: BaseRepository<Survey, DataContext>, ISurveyRepository
{
    public SurveyRepository(DataContext context) : base(context)
    {
    }
}