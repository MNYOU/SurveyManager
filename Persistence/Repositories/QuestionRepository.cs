using Domain.Entities;
using DomainServices.Repositories;

namespace Persistence.Repositories;

public class QuestionRepository: EntityRepository<Question, DataContext>, IQuestionRepository
{
    public QuestionRepository(DataContext context) : base(context)
    {
    }
}