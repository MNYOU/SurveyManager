using Application.Models.Requests.Survey;
using Application.Models.Responses.Survey;
using Infrastructure.Common.Result;

namespace Application.Services;


// TODO отделельные модели для прохождения и для просмотра(админом, аналитиком)
public interface ISurveyService
{
    public Task<Result> CreateAsync(Guid adminId, CreateSurveyRequest request);

    public Result Update(Guid adminId, Guid surveyId, CreateSurveyRequest request);

    public Result Delete(DeleteSurveyRequest request);
    
    public Result<SurveyView> GetSurveyResult(Guid id);

    public Result<IEnumerable<SurveyPreview>> GetSurveysPreviewByAdmin(Guid adminId);
}