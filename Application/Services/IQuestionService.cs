using Domain.Entities;

namespace Application.Services;

public interface IQuestionService
{
    public Task<List<Question>> GetDefaultQuestionsAsync();

    public Task<Question?> GetAsync(Guid id);

}