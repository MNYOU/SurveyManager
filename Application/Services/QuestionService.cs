using AutoMapper;
using Domain.Entities;
using DomainServices.Repositories;
using Infrastructure.Common.Logging;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class QuestionService: IQuestionService
{
    private readonly IQuestionRepository _repository;
    private IMapper _mapper;
    private ICustomLogger _logger;

    public QuestionService(IQuestionRepository repository, IMapper mapper, ICustomLogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public Task<List<Question>> GetDefaultQuestionsAsync()
    {
        return Task.FromResult(_repository.Items
            .Include(e => e.Survey)
            .Include(e => e.Options)
            .Where(e => e.IsDefault).ToList());
    }

    public async Task<Question?> GetAsync(Guid id)
    {
        return await _repository.Items
            .Include(e => e.Survey)
            .Include(e => e.Options)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}