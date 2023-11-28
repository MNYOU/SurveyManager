using Application.Models.Requests.Survey;
using Application.Models.Responses.Survey;
using Infrastructure.Common.Result;

namespace Application.Services;

public class SurveyService: ISurveyService
{
    public async Task<Result> CreateAsync(Guid adminId, CreateSurveyRequest request)
    {
        await AddDefaultQuestions(request);
        throw new NotImplementedException();
    }
    
    private async Task AddDefaultQuestions(CreateSurveyRequest request)
    {
        throw new NotImplementedException();
        // TODO добавить 2 дефолтных вопроса
    }

    public Result Update(Guid adminId, Guid surveyId, CreateSurveyRequest request)
    {
        throw new NotImplementedException();
    }

    public Result Delete(DeleteSurveyRequest request)
    {
        throw new NotImplementedException();
    }

    public Result<SurveyView> GetSurveyResult(Guid id)
    {
        throw new NotImplementedException();
    }

    public Result<IEnumerable<SurveyPreview>> GetSurveysPreviewByAdmin(Guid adminId)
    {
        throw new NotImplementedException();
    }
}