using Application.Common.Result.Validation;
using Application.Models.Requests.Analyst;
using Application.Models.Responses.Statistics;
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

public class AnalystService: IAnalystService
{
    private readonly IMapper _mapper;    
    private readonly ICustomLogger _logger;
    private readonly IAnalystRepository _repository;
    private readonly IAccountService _accountService;
    private readonly IAdminService _adminService;
    private readonly IPatientRepository _patientRepository;
    private readonly IPatientSurveyRepository _surveyRepository;

    public AnalystService(IMapper mapper, ICustomLogger logger, IPatientRepository patientRepository, IPatientSurveyRepository surveyRepository, IAnalystRepository repository, IAccountService accountService, IAdminService adminService)
    {
        _mapper = mapper;
        _logger = logger;
        _patientRepository = patientRepository;
        _surveyRepository = surveyRepository;
        _repository = repository;
        _accountService = accountService;
        _adminService = adminService;
    }
    
    private async Task<Analyst?> CheckAnalystAsync(Guid id)
    {
        var user = await _accountService.GetUserByIdAsync(id);
        if (user == null) return null;
        var analyst = await _repository.Items
            .Include(e => e.Accesses)
            .FirstOrDefaultAsync(e => e.Id == id);
        return analyst;
    }

    public async Task<Result<IEnumerable<SurveyPreview>>> GetAvailableSurveys(Guid analystId)
    {
        var analyst = await CheckAnalystAsync(analystId);
        if (analyst is null)
            return Result.Unauthorized();

        var surveys = new HashSet<Survey>();
        foreach (var access in analyst.Accesses)
        {
            // _surveyRepository.Items.
            var admin = await _adminService.FindByAccessKey(access.AccessKey);
            if (admin is null)
                continue;
            foreach (var survey in admin.Surveys)
                surveys.Add(survey);;
        }


        return Result.Success(surveys.Select(e => _mapper.Map<SurveyPreview>(e)));
    }

    public async Task<Result> AddAccessToSurveys(Guid userId, Guid key)
    {
        var analyst = await CheckAnalystAsync(userId);
        if (analyst is null)
        {
            _logger.Log(LogLevel.Information, $"Аналитик не найден. id: {userId}");
            return Result.Forbidden();
        }

        var admin = await _adminService.FindByAccessKey(key);
        if (admin is null)
        {
            _logger.Log(LogLevel.Information, $"Ключ не найден. id: {userId}");
            return Result.Invalid(new List<ValidationError>());
        }
        
        analyst.Accesses.Add(new AnalystAccess() {AccessKey = key});
        _repository.Update(analyst);
        try
        {
            await _repository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return Result.Error("Ошибка при записи доступа", e.Message);
        }

        return Result.Success();
    }

    public async Task<Result<SurveyStats>> GetSurveyStatsForAllAnswers(SurveyStatsFilters filters, Guid userId)
    {
        var analyst = await CheckAnalystAsync(userId);
        if (analyst is null)
        {
            _logger.Log(LogLevel.Information, $"Аналитик не найден. id: {userId}");
            return Result.Forbidden();
        }

        var answers = _surveyRepository.Items
            .Include(e => e.Survey)
            .Include(e => e.Answers)
            .ThenInclude(e => e.Question)
            .Include(e => e.Answers)
            .ThenInclude(e => e.SelectedAnswerOptions)
            .ThenInclude(e => e.Question)
            .Where(e => e.SurveyId == filters.SurveyId);
            // .SelectMany(e => e.Answers)
            // .AsEnumerable();
        // .Where(e => e.);

        return Result.Success(CalculateStatistics(answers));
    }

    private SurveyStats CalculateStatistics(IEnumerable<PatientSurveyAnswer> surveyAnswers)
    {
        if (!surveyAnswers.Any())
            return null;
        
        var survey = surveyAnswers.First();
        var stats = new SurveyStats()
        {
            SurveyId = survey.SurveyId.Value,
            Survey = _mapper.Map<SurveyPreview>(survey.Survey),
            Questions = new List<QuestionStats>(),
        };

        var d = new Dictionary<Guid, List<PatientAnswer>>();
        foreach (var surveyAnswer in surveyAnswers)
        {
            var filtered = FilterAnswers(surveyAnswer.Answers);
            foreach (var answer in filtered)
            {
                if (!d.ContainsKey(answer.QuestionId))
                    d[answer.QuestionId] = new List<PatientAnswer>();
                d[answer.QuestionId].Add(answer);
            }
        }

        foreach (var pair in d)
        {
            stats.Questions.Add(CalculateQuestionStats(pair.Value));
        }

        return stats;
    }

    private QuestionStats CalculateQuestionStats(IEnumerable<PatientAnswer> patientAnswers)
    {
        var answers = patientAnswers.ToArray();
        if (!answers.Any())
            return null;

        var anyAnswers = answers[0];
        var stats = new QuestionStats()
        {
            QuestionId = anyAnswers.QuestionId,
            Question = _mapper.Map<QuestionView>(anyAnswers.Question),
            AnswerStats = new List<AnswerOptionStats>(),
        };

        if (anyAnswers.Question.Type is QuestionType.Once or QuestionType.Range)
        {
            var d = new Dictionary<Guid, int>();
            foreach (var answer in answers)
            {
                foreach (var option in answer.SelectedAnswerOptions)
                {
                    if (!d.ContainsKey(option.Id))
                        d[option.Id] = 0;
                    d[option.Id] += 1;
                }
            }

            foreach (var pair in d)
            {
                var optionStats = new AnswerOptionStats()
                {
                    AnswerOptionId = pair.Key,
                    Answer = _mapper.Map<AnswerOptionView>(answers.SelectMany(e => e.SelectedAnswerOptions).First(e => e.Id == pair.Key)),
                    Count = pair.Value,
                    Percent = (int)(pair.Value * 100.0 / answers.Length),
                };
                stats.AnswerStats.Add(optionStats);
            }
        }
        else if (anyAnswers.Question.Type is QuestionType.Range)
        {
            
        }

        return stats;
    }

    private IEnumerable<PatientAnswer> FilterAnswers(IEnumerable<PatientAnswer> patientAnswers)
    {
        var answers = patientAnswers.ToList();
        var filtered = new List<PatientAnswer>();
        foreach (var answer in answers)
        {
            if (answer.Question.IsDefault)
                continue;
            
            if (HasAnswer(answer))
                filtered.Add(answer);
            // switch (answer.Question.Type)
            // {
            //     case QuestionType.Text:
            //         break;
            //     case QuestionType.Once:
            //     case QuestionType.Select:
            //         if (answer.SelectedAnswerOptions.Any())
            //             filtered.Add(answer);
            //         break;
            //     case QuestionType.Range:
            //         if (answer.RangeAnswer.HasValue)
            //             filtered.Add(answer);
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        }

        return filtered;
    }

    private bool HasAnswer(PatientAnswer answer)
    {
        switch (answer.Question.Type)
        {
            case QuestionType.Text:
                break;
            case QuestionType.Once:
            case QuestionType.Select:
                if (answer.SelectedAnswerOptions.Any())
                    return true;
                break;
            case QuestionType.Range:
                if (answer.RangeAnswer.HasValue)
                    return true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return false;
    }
}