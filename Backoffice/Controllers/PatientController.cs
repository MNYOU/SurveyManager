using Application.Models.Requests.Survey;
using Application.Models.Responses.Account;
using Application.Models.Responses.Survey;
using Application.Services;
using AutoMapper;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

[Route("[controller]")]
public class PatientController: ApiBaseController
{
    private readonly ISurveyService _surveyService;
    
    public PatientController(ICustomLogger logger, IMapper mapper, ISurveyService surveyService) : base(logger, mapper)
    {
        _surveyService = surveyService;
    }
    
    [AllowAnonymous]
    [HttpGet("surveys/{key:guid}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(IEnumerable<SurveyPreview>), 200)]
    public async Task<Result<IEnumerable<SurveyPreview>>> GetAvailableSurveys([FromRoute] Guid key)
    {
        var result = await _surveyService.GetSurveysPreviewForPass(key);

        return result;
    }

    [AllowAnonymous]
    [HttpGet("survey/{surveyId:guid}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SurveyView), 200)]
    public async Task<Result<SurveyView>> GetSurveyForPass([FromRoute] Guid surveyId)
    {
        var result = await _surveyService.GetSurveyForPass(surveyId);

        return result;
    }
    
    [AllowAnonymous]
    // [HttpPost("survey/{surveyId:guid}")]
    [HttpPost("survey/")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    // [ProducesResponseType(typeof(Result), 200)]
    public async Task<Result> UploadSurveyData([FromBody] SurveyRequest request)
    {
        var result = await _surveyService.UploadSurveyPassData(request);

        return result;
    }
}