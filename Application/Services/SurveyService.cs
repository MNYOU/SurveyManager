using Application.Common.Result.Validation;
using Application.Models.Requests.Survey;
using Application.Models.Responses.Survey;
using Application.Services.Account;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using DomainServices.Repositories;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class SurveyService: ISurveyService
{
    private readonly ISurveyRepository _repository;
    private readonly IAccountService _accountService;
    private readonly IQuestionService _questionService;
    private readonly IPatientSurveyRepository _patientSurveyRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly ICustomLogger _logger;
    private readonly IMapper _mapper;

    public SurveyService(ISurveyRepository repository, IAccountService accountService, ICustomLogger logger, IMapper mapper, IQuestionService questionService, IPatientRepository patientRepository, IPatientSurveyRepository patientSurveyRepository)
    {
        _repository = repository;
        _accountService = accountService;
        _logger = logger;
        _mapper = mapper;
        _questionService = questionService;
        _patientRepository = patientRepository;
        _patientSurveyRepository = patientSurveyRepository;
    }

    private bool CheckAccess(Survey survey, Guid userId)
    {
        // TODO сейчас мы проверяем только админа, добавить проверку доступа аналитика
        return survey.AdminId == userId;
    }
    
    [Obsolete]
    private async Task<Result<Survey>> GetSurvey(Guid id, Guid userId)
    {
        var survey = await _repository.Items.FirstOrDefaultAsync(e => e.Id == id);
        if (survey == null) return Result.Error("Опрос не найден!");
        if (!CheckAccess(survey, userId)) return Result.Forbidden();
        return Result.Success(survey);
    }

    private bool IsValid(Survey survey)
    {
        return true;
        // проверка, что у вопросов правильная последовательность
        // throw new NotImplementedException();
    }
    
    
    public async Task<Result<SurveyView>> GetSurveyAsync(Guid id, Guid userId)
    {
        var survey = await _repository.Items
            .Include(e => e.Questions)
            .ThenInclude(e => e.Options)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (survey == null) return Result.Error("Опрос не найден!");
        if (!CheckAccess(survey, userId)) return Result.Forbidden();
        
        return Result.Success(_mapper.Map<SurveyView>(survey));
    }

    public async Task<Result> CreateAsync(Guid adminId, CreateSurveyRequest request)
    {
        if (!await _accountService.CheckUserInRoleAsync(adminId, RolesEnum.Admin))
            return Result.Forbidden();
        
        var survey = _mapper.Map<Survey>(request);
        survey.AdminId = adminId;
        survey.ContainsDefaultQuestions = true;
        survey.CreationTime = DateTime.Now;
        
        if (survey.Questions.Any(q => q.IsDefault))
            return Result.Forbidden();
        if (!IsValid(survey))
            return Result.Invalid(new List<ValidationError>()); // 0 ошибок?

        await _repository.Items.AddAsync(survey);
        try
        {
            await _repository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.Error("Ошибка при загрузке опроса", e.Message);
        }

        return Result.Success();
    }

    public Result Update(Guid adminId, Guid surveyId, CreateSurveyRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(DeleteSurveyRequest request)
    {
        var survey = await _repository.Items.FirstOrDefaultAsync(e => e.Id == request.SurveyId);
        if (survey == null) return Result.Error("Опрос не найден!");
        if (!CheckAccess(survey, request.AdminId)) return Result.Forbidden();
        
        try
        {
            _repository.Delete(survey);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.Error("Ошибка при удалении опроса", e.Message);
        }

        return Result.Success();
    }

    public Task<Result<IEnumerable<SurveyPreview>>> GetSurveysPreviewByAdmin(Guid adminId)
    {
        var surveys = _repository.Items.Where(e => e.AdminId == adminId);

        var data = surveys
            .Select(x => _mapper.Map<SurveyPreview>(x))
            .AsEnumerable();
        return Task.FromResult(Result.Success(data));
    }

    public async Task<Result<IEnumerable<SurveyPreview>>> GetSurveysPreviewForPass()
    {
        // TODO наверное переделать
        return Result.Success(_repository.Items
            .Select(x => _mapper.Map<SurveyPreview>(x))
            .AsEnumerable());
    }

    public async Task<Result<SurveyView>> GetSurveyForPass(Guid id)
    {
        var survey = await GetSurveyWithDefaultQuestions(id);
        if (survey == null)
        {
            return Result.Error();
        }
        
        return Result.Success(_mapper.Map<SurveyView>(survey));
        // if () 
        throw new NotImplementedException();
    }

    private async Task<Survey?> GetSurveyWithDefaultQuestions(Guid id)
    {
        var survey =await _repository.Items
            .Include(e => e.Questions)
            .ThenInclude(e => e.Options)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (survey is { ContainsDefaultQuestions: true })
        {
            var defaultQuestions = await _questionService.GetDefaultQuestionsAsync();
            foreach (var question in defaultQuestions)
            {
                survey.Questions.Add(question);
            }
            // TODO
            // var defaultQuestions = questi
        }

        return survey;
    }

    public async Task<Result> UploadSurveyPassData(SurveyRequest request)
    {
        var patientSurvey = new PatientSurveyAnswer() { SurveyId = request.Id };
        var date = DateTime.UtcNow;
        var patientAnswers = new List<PatientAnswer>();
        // await _patientSurveyRepository.Items.AddAsync(patientSurvey);
        // await _patientSurveyRepository.UnitOfWork.SaveChangesAsync();
        foreach (var questionRequest in request.Questions)
        {
            var question = await _questionService.GetAsync(questionRequest.Id);
            if (question == null) return Result.Error();
            var patientAnswer = new PatientAnswer()
            {
                SurveyAnswer = patientSurvey,
                QuestionId = questionRequest.Id,
                Date = date,
            };
            
            switch (question.Type)
            {
                case QuestionType.Once:
                    if (questionRequest.SelectedOptions is not { Count: 1 })
                        return Result.Error();
                    
                    patientAnswer.SelectedAnswerOptions = new List<AnswerOption>();
                    var option =
                        question.Options.FirstOrDefault(e => questionRequest.SelectedOptions.Any(o => o.Id == e.Id));
                    if (option is null && question.IsRequired)
                        return Result.Error();

                    if (option != null) 
                        patientAnswer.SelectedAnswerOptions.Add(option);
                    break;
                case QuestionType.Select:
                    if (questionRequest.SelectedOptions is null || !questionRequest.SelectedOptions.Any())
                        return Result.Error();
                    
                    patientAnswer.SelectedAnswerOptions = new List<AnswerOption>();
                    var options =
                        question.Options.Where(e => questionRequest.SelectedOptions.Any(o => o.Id == e.Id));
                    if (!options.Any() && question.IsRequired)
                        return Result.Error();
                    
                    foreach (var answerOption in options)
                        patientAnswer.SelectedAnswerOptions.Add(answerOption);
                    break;
                case QuestionType.Text:
                    if (questionRequest.TextAnswer != null)
                        patientAnswer.TextAnswer = questionRequest.TextAnswer;
                    else if (question.IsRequired)
                        return Result.Error();
                    break;
                case QuestionType.Range:
                    if (questionRequest.RangeValue != null)
                        patientAnswer.RangeAnswer = questionRequest.RangeValue;
                    else if (question.IsRequired)
                        return Result.Error();
                    break;
                default:
                    return Result.Error();
            }
            
            patientAnswers.Add(patientAnswer);
        }

        await _patientRepository.Items.AddRangeAsync(patientAnswers);
        try
        {
            await _patientRepository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Result.Error();
        }

        return Result.Success();
    }
}