using Domain.Entities;
using Domain.Entities.Base;
using DomainServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class SurveyRepository: EntityRepository<Survey, DataContext>, ISurveyRepository
{
    public SurveyRepository(DataContext context) : base(context)
    {
    }
}