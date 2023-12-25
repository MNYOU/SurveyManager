using Application.Models.Requests.Analyst;
using Application.Models.Responses.Statistics;
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
[Authorize(Roles = nameof(RolesEnum.Analyst))]
public class AnalystController:ApiBaseController
{
    private IAnalystService _service;
    
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

    [HttpGet("survey/")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(SurveyStats), 200)]
    public Task<Result<SurveyStats>> GetSurveyStats([FromQuery] SurveyStatsFilters filters)
    {
        return _service.GetSurveyStatsForAllAnswers(filters, AuthorizedUser.Id);
    }
    
    [HttpPost("surveys/add")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    // [ProducesResponseType(typeof(SurveyVie>), 200)]
    public Task<Result> AddAccessToSurveys([FromQuery] Guid accessKey)
    {
        var result = _service.AddAccessToSurveys(AuthorizedUser.Id, accessKey);

        return result;
    }
}