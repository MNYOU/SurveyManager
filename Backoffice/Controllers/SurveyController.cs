using Application.Models.Requests.Survey;
using Application.Models.Responses.Account;
using Application.Models.Responses.Survey;
using Application.Services;
using AutoMapper;
using Domain.Enums;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

[Route("[controller]/")]
public class SurveyController: ApiBaseController
{
    private readonly ISurveyService _surveyService;

    public SurveyController(ISurveyService surveyService, ICustomLogger logger, IMapper mapper): base(logger, mapper)
    {
        _surveyService = surveyService;
    }
    
    [HttpGet("{surveyId:guid}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SurveyView), 200)]
    public async Task<Result<SurveyView>> GetAsync([FromRoute] Guid surveyId)
    {
        var result = await _surveyService.GetSurveyAsync(surveyId, AuthorizedUser.Id);

        return result;
    }
    
    [HttpPost]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(CreateSurveyRequest), 200)]
    public async Task<Result<CreateSurveyRequest>> CreateAsync([FromBody] CreateSurveyRequest request)
    {
        var result = await _surveyService.CreateAsync(AuthorizedUser.Id, request);

        return result;
    }
    
    [HttpPut("{surveyId:guid}")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SurveyView), 200)]
    public Result<SurveyView> Update([FromRoute] Guid surveyId, [FromBody] CreateSurveyRequest request)
    {
        var result = _surveyService.Update(AuthorizedUser.Id, surveyId, request);

        return result;
    }
        
    [HttpDelete("{surveyId:guid}")]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SurveyView), 200)]
    public async Task<Result<SurveyView>> DeleteAsync([FromRoute] Guid surveyId)
    {
        var result = await _surveyService.DeleteAsync(new DeleteSurveyRequest(AuthorizedUser.Id, surveyId));

        return result;
    }
    
}