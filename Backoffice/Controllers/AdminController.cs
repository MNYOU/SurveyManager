using Application.Models.Responses.Admin;
using Application.Models.Responses.Survey;
using Application.Services;
using AutoMapper;
using Domain.Enums;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

[Authorize(Roles = nameof(RolesEnum.Admin))]
[Route("[controller]")]
public class AdminController: ApiBaseController
{
    private readonly IAdminService _adminService;
    private readonly ISurveyService _surveyService;
    
    public AdminController(ICustomLogger logger, IMapper mapper, ISurveyService surveyService, IAdminService adminService) : base(logger, mapper)
    {
        _surveyService = surveyService;
        _adminService = adminService;
    }
    
    [HttpGet("/surveys")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(IEnumerable<SurveyPreview>), 200)]
    public Result<IEnumerable<SurveyPreview>> Get()
    {
        var result = _surveyService.GetSurveysPreviewByAdmin(AuthorizedUser.Id);

        return result;
    }
    
    [HttpGet("/surveys/access-key")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(AccessKey), 200)]
    public async Task<Result<AccessKey>> GetAccessKeyAsync()
    {
        var result = await _adminService.GetAccessCode(AuthorizedUser.Id);

        return result;
    }
    
    [HttpPut("/surveys/access-key")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<Result> UpdateAccessKeyAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("/surveys/link")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(LinkSurveys), 200)]
    public async Task<Result<LinkSurveys>> GetLinkForSurveys()
    {
        throw new NotImplementedException();
    }
    
    // [HttpDelete("/surveys/delete-all-accesses")]
    // [TranslateResultToActionResult]
    // [ProducesDefaultResponseType(typeof(Result))]
    // public async Task<Result> DeleteAllAccessesAsync()
    // {
        // throw new NotImplementedException();
    // }
}