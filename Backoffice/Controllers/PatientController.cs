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
    [HttpGet("survey/{surveyId:guid}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SurveyView), 200)]
    public Result<SurveyView> GetSurveyForPass([FromRoute] Guid surveyId)
    {
        throw new NotImplementedException();
    }
    
    [AllowAnonymous]
    [HttpPost("survey/{surveyId:guid}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    // [ProducesResponseType(typeof(Result), 200)]
    public Result UploadSurveyData([FromBody] SurveyRequest request)
    {
        // var result = Result.Forbidden();
        // if (result.Failed)
            // ModelState.AddModelError(nameof(request.Id), "Опрос не найден");
        // return result;
        throw new NotImplementedException();
    }
}