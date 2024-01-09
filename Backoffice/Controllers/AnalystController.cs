using Application.Models.Requests.Analyst;
using Application.Models.Responses.Statistics;
using Application.Models.Responses.Statistics.Average;
using Application.Models.Responses.Survey;
using Application.Services;
using AutoMapper;
using Domain.Enums;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

[Route("[controller]")]
[Authorize(Roles = $"{nameof(RolesEnum.Analyst)},{nameof(RolesEnum.Admin)}")]
public class AnalystController:ApiBaseController
{
    private readonly IAnalystService _service;
    
    public AnalystController(ICustomLogger logger, IMapper mapper, IAnalystService service) : base(logger, mapper)
    {
        _service = service;
    }
    
    [HttpGet("surveys")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(IEnumerable<SurveyPreview>), 200)]
    public Task<Result<IEnumerable<SurveyPreview>>> GetAvailableSurveys()
    {
        return _service.GetAvailableSurveys(AuthorizedUser.Id);
    }

    [HttpGet("survey/all")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SurveyStats), 200)]
    public Task<Result<SurveyStats>> GetSurveyStatsAllAnswers([FromQuery] SurveyStatsFilters filters)
    {
        return _service.GetSurveyStatsForAllAnswers(filters, AuthorizedUser.Id);
    }
    
    [HttpGet("survey/average")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SurveyStats), 200)]
    public Task<Result<SurveyAverageStats>> GetSurveyAverageStats([FromQuery] SurveyStatsFilters filters)
    {
        return _service.GetSurveyAverageStats(filters, AuthorizedUser.Id);
    }
    
    [HttpPost("surveys/add")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public Task<Result> AddAccessToSurveys([FromQuery] Guid accessKey)
    {
        var result = _service.AddAccessToSurveys(AuthorizedUser.Id, accessKey);

        return result;
    }
    
    [AllowAnonymous]
    [HttpGet("department")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(IEnumerable<DepartmentView>), 200)]
    public Task<Result<IEnumerable<DepartmentView>>> GetDepartments()
    {
        return _service.GetDepartments();
    }
}