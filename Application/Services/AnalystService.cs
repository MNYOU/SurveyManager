using System.Net.Mime;
using Application.Common.Result.Validation;
using Application.Models.Requests.Analyst;
using Application.Models.Responses.Statistics;
using Application.Models.Responses.Statistics.AllAnswers;
using Application.Models.Responses.Statistics.Average;
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

// todo разделить на несколько классов с разной статистикой
public class AnalystService : IAnalystService
{
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;
    private readonly IAdminService _adminService;
    private readonly IAnalystRepository _repository;
    private readonly IAccountService _accountService;
    private readonly ISurveyRepository _surveyRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPatientSurveyRepository _surveyAnswerRepository;

    public AnalystService(IMapper mapper, ICustomLogger logger, IPatientRepository patientRepository,
        IPatientSurveyRepository surveyAnswerRepository, IAnalystRepository repository, IAccountService accountService,
        IAdminService adminService, ISurveyRepository surveyRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _patientRepository = patientRepository;
        _surveyAnswerRepository = surveyAnswerRepository;
        _repository = repository;
        _accountService = accountService;
        _adminService = adminService;
        _surveyRepository = surveyRepository;
    }

    private async Task<bool> CheckAccess(Guid userId, Guid surveyId)
    {
        var user = await _accountService.GetUserByIdAsync(userId);
        if (user is null)
            return false;

        var survey = await _surveyRepository.Items
            .Include(e => e.Admin)
            .FirstOrDefaultAsync(e => e.Id == surveyId);
        switch (user.Role)
        {
            case RolesEnum.Admin:
                return survey?.AdminId == userId;
            case RolesEnum.Analyst:
                var analyst = await _repository.Items
                    .Include(e => e.Accesses)
                    .FirstOrDefaultAsync(e => e.Id == userId);
                if (survey is null || analyst is null)
                    return false;

                return analyst.Accesses.Any(a => a.AccessKey == survey.Admin.AccessKey);
            default:
                return false;
        }
    }


    private async Task<Analyst?> CheckAnalystAccessAsync(Guid id)
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
        var surveys = new HashSet<Survey>();
        
        var accessResult = await _adminService.GetAccessCode(analystId);
        if (accessResult.IsSuccess)
        {
            var admin = await _adminService.FindByAccessKey(accessResult.Data.Key);
            if (admin is not null)
            {
                foreach (var survey in admin.Surveys)
                    surveys.Add(survey);
                
                return Result.Success(surveys.Select(e => _mapper.Map<SurveyPreview>(e)));
            }
        }

        
        var analyst = await CheckAnalystAccessAsync(analystId);
        if (analyst is null)
            return Result.Unauthorized();

        foreach (var access in analyst.Accesses)
        {
            // _surveyAnswerRepository.Items.
            var admin = await _adminService.FindByAccessKey(access.AccessKey);
            if (admin is null)
                continue;
            foreach (var survey in admin.Surveys)
                surveys.Add(survey);
        }


        return Result.Success(surveys.Select(e => _mapper.Map<SurveyPreview>(e)));
    }

    public async Task<Result> AddAccessToSurveys(Guid userId, Guid key)
    {
        var analyst = await CheckAnalystAccessAsync(userId);
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

        analyst.Accesses.Add(new AnalystAccess() { AccessKey = key });
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

    private IQueryable<PatientSurveyAnswer> GetAnswers(SurveyStatsFilters filters)
    {
        return _surveyAnswerRepository.Items
            .Include(e => e.Survey)
            .Include(e => e.Answers)
            .ThenInclude(e => e.Question)
            .Include(e => e.Answers)
            .ThenInclude(e => e.SelectedAnswerOptions)
            .ThenInclude(e => e.Question)
            .Where(e => e.SurveyId == filters.SurveyId);
    }

    public async Task<Result<SurveyStats>> GetSurveyStatsForAllAnswers(SurveyStatsFilters filters, Guid userId)
    {
        var access = await CheckAccess(userId, filters.SurveyId);
        if (!access)
        {
            _logger.Log(LogLevel.Information, $"Доступ не найден. id: {userId}");
            return Result.Forbidden();
        }

        var answers = GetAnswers(filters);

        return Result.Success(CalculateStatistics(answers, filters));
    }

    public async Task<Result<SurveyAverageStats>> GetSurveyAverageStats(SurveyStatsFilters filters, Guid userId)
    {
        var access = await CheckAccess(userId, filters.SurveyId);
        if (!access)
        {
            _logger.Log(LogLevel.Information, $"Доступ не найден. id: {userId}");
            return Result.Forbidden();
        }

        var answers = GetAnswers(filters);

        return Result.Success(CalculateAverageStatistics(answers, filters));
    }

    private SurveyAverageStats CalculateAverageStatistics(IEnumerable<PatientSurveyAnswer> surveyAnswers, SurveyStatsFilters filters)
    {
        if (!surveyAnswers.Any())
            return null;

        var survey = surveyAnswers.First();
        var stats = new SurveyAverageStats()
        {
            // SurveyId = survey.SurveyId.Value,
            Survey = _mapper.Map<SurveyPreview>(survey.Survey), // todo if null
            Questions = new List<QuestionAverageStats>(),
        };

        var questionAnswers = new Dictionary<Guid, List<PatientAnswer>>();
        foreach (var surveyAnswer in surveyAnswers)
        {
            var filtered = FilterAnswers(surveyAnswer.Answers, filters);
            foreach (var answer in filtered)
            {
                if (!questionAnswers.ContainsKey(answer.QuestionId ?? Guid.Empty)) // todo
                    questionAnswers[answer.QuestionId ?? Guid.Empty] = new List<PatientAnswer>();
                questionAnswers[answer.QuestionId ?? Guid.Empty].Add(answer);
            }
        }

        foreach (var pair in questionAnswers)
        {
            stats.Questions.Add(CalculateQuestionAverageStats(pair.Value));
        }

        return stats;
    }
    
    private QuestionAverageStats CalculateQuestionAverageStats(IEnumerable<PatientAnswer> patientAnswers)
    {
        var answers = patientAnswers.ToArray();
        if (!answers.Any())
            return null;

        var question = answers[0].Question;
        var stats = new QuestionAverageStats()
        {
            Question = _mapper.Map<QuestionView>(question),
        };
        
        switch (question.Type) // TODo
        {
            case QuestionType.Text:
                var texts = answers
                    .Select(e => e.TextAnswer)
                    .ToArray();
                stats.TextAnswersStats = new TextAnswersStats()
                {
                    Count = texts.Length,
                    Answers = new List<string>(texts),
                };
                break;
            case QuestionType.Once:
            case QuestionType.Select:
                var frequency = new Dictionary<Guid, int>();
                foreach (var answer in answers)
                {
                    foreach (var option in answer.SelectedAnswerOptions)
                    {
                        frequency.TryAdd(option.Id, 0);
                        frequency[option.Id] += 1;
                    }
                }

                var max = frequency.MaxBy(pair => pair.Value);
                stats.OptionStats = new AnswerOptionStats()
                {
                    AnswerOptionId = max.Key,
                    Answer = _mapper.Map<AnswerOptionView>(question.Options.First(e => e.Id == max.Key)),
                    Count = max.Value,
                    Percent = (int)Math.Round(max.Value * 100.0 / answers.Length),
                };
                break;
            case QuestionType.Range:
                var frequency2 = new Dictionary<int, int>();
                foreach (var answer in answers)
                {
                    var rangeValue = answer.RangeAnswer.Value;
                    frequency2.TryAdd(rangeValue, 0);
                    frequency2[rangeValue] += 1;
                }

                var max2 = frequency2.MaxBy(pair => pair.Value);
                stats.AverageRange = new RangeStats()
                {
                    Count = max2.Value,
                    RangeValue = max2.Key,
                    Percent = (int)Math.Round(max2.Value * 100.0 / answers.Length),
                };
                break;
            default:
                throw new InvalidOperationException();
        }

        return stats;
    }

    private SurveyStats CalculateStatistics(IEnumerable<PatientSurveyAnswer> surveyAnswers, SurveyStatsFilters filters)
    {
        if (!surveyAnswers.Any())
            return null;

        var survey = surveyAnswers.First();
        var stats = new SurveyStats()
        {
            // SurveyId = survey.SurveyId.Value,
            Survey = _mapper.Map<SurveyPreview>(survey.Survey),
            Questions = new List<QuestionStats>(),
        };

        var d = new Dictionary<Guid, List<PatientAnswer>>();
        foreach (var surveyAnswer in surveyAnswers)
        {
            var filtered = FilterAnswers(surveyAnswer.Answers, filters);
            foreach (var answer in filtered)
            {
                if (!d.ContainsKey(answer.QuestionId ?? Guid.Empty))
                    d[answer.QuestionId ?? Guid.Empty] = new List<PatientAnswer>();
                d[answer.QuestionId ?? Guid.Empty].Add(answer);
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

        var question = answers[0].Question;
        var stats = new QuestionStats
        {
            Question = _mapper.Map<QuestionView>(question),
            AnswerStats = new List<AnswerOptionStats>()
        };

        if (question.Type is QuestionType.Once or QuestionType.Select)
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
                    Answer = _mapper.Map<AnswerOptionView>(answers.SelectMany(e => e.SelectedAnswerOptions)
                        .First(e => e.Id == pair.Key)),
                    Count = pair.Value,
                    Percent = (int)Math.Round(pair.Value * 100.0 / answers.Length),
                };
                stats.AnswerStats.Add(optionStats);
            }
        }
        else if (question.Type is QuestionType.Range)
        {
            var frequency = new Dictionary<int, int>();
            for (var i = 0; i < 10; i++)
                frequency[i + 1] = 0;

            foreach (var answer in answers)
            {
                var rangeValue = answer.RangeAnswer.Value;
                frequency.TryAdd(rangeValue, 0);
                frequency[rangeValue] += 1;
            }

            var rangeStats = new RangeAllValuesStats()
            {
                RangeList = new List<RangeStats>(),
            };
            foreach (var pair in frequency)
            {
                rangeStats.RangeList.Add(new RangeStats()
                {
                    Count = pair.Value,
                    RangeValue = pair.Key,
                    Percent = (int)Math.Round(pair.Value * 100.0 / answers.Length),
                });
            }
            
            rangeStats.RangeList = rangeStats
                .RangeList.OrderBy(e => e.RangeValue)
                .ToList();
            stats.RangeStats = rangeStats;
        }
        else if (question.Type is QuestionType.Text)
        {
            var texts = answers
                .Select(e => e.TextAnswer)
                .ToArray();
            stats.TextAnswersStats = new TextAnswersStats()
            {
                Count = texts.Length,
                Answers = new List<string>(texts),
            };
        }

        return stats;
    }

    private IEnumerable<PatientAnswer> FilterAnswers(IEnumerable<PatientAnswer> patientAnswers, SurveyStatsFilters filters)
    {
        var answers = patientAnswers.ToList();
        var filtered = new List<PatientAnswer>();
        foreach (var answer in answers)
        {
            if (!answer.QuestionId.HasValue || answer.Question.IsDefault)
                continue;
            
            if (!HasAnswer(answer))
                continue;
            
            var passDate = DateOnly.FromDateTime(answer.Date);
            if (filters.To.HasValue && passDate > filters.To)
                continue;
            if (filters.From.HasValue && passDate < filters.From)
                continue;
            
            filtered.Add(answer);
        }

        return filtered;
    }

    // public IEnumerable<>

    private bool HasAnswer(PatientAnswer answer)
    {
        if (answer.Question is null)
            return false;
        
        switch (answer.Question.Type)
        {
            case QuestionType.Text:
                var text = answer.TextAnswer;
                return !string.IsNullOrWhiteSpace(text);
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
                return false;
        }

        return false;
    }
}