using Application.Models.Requests.Survey;
using Application.Models.Responses.Survey;
using Infrastructure.Common.Result;

namespace Application.Services;


// TODO отделельные модели для прохождения и для просмотра(админом, аналитиком)
public interface ISurveyService
{
    public Task<Result> CreateAsync(Guid adminId, CreateSurveyRequest request);

    public Task<Result> Update(Guid adminId, Guid surveyId, CreateSurveyRequest request);

    public Task<Result> DeleteAsync(DeleteSurveyRequest request);
    
    public Task<Result<SurveyView>> GetSurveyAsync(Guid id, Guid userId);
    
    public Task<Result<IEnumerable<SurveyPreview>>> GetSurveysPreviewByAdmin(Guid adminId);
    
    public Task<Result<IEnumerable<SurveyPreview>>> GetSurveysPreviewForPass();
    
    public Task<Result<SurveyView>> GetSurveyForPass(Guid id);

    public Task<Result> UploadSurveyPassData(SurveyRequest request);
}