using Application.Models.Requests.Analyst;
using Application.Models.Responses.Statistics;
using Application.Models.Responses.Statistics.Average;
using Application.Models.Responses.Survey;
using Infrastructure.Common.Result;

namespace Application.Services;

public interface IAnalystService
{
    public Task<Result<IEnumerable<SurveyPreview>>> GetAvailableSurveys(Guid analystId);

    public Task<Result> AddAccessToSurveys(Guid userId, Guid key);

    public Task<Result<SurveyStats>> GetSurveyStatsForAllAnswers(SurveyStatsFilters filters, Guid userId);

    public Task<Result<SurveyAverageStats>> GetSurveyAverageStats(SurveyStatsFilters filters, Guid userId);

    public Task<Result<IEnumerable<DepartmentView>>> GetDepartments();
}