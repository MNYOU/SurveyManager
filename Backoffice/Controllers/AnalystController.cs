using Application.Models.Requests.Analyst;
using Application.Models.Responses.Survey;
using AutoMapper;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

[Route("[controller]")]
public class AnalystController:ApiBaseController
{
    public AnalystController(ICustomLogger logger, IMapper mapper) : base(logger, mapper)
    {
    }

    [HttpGet("survey/")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    // [ProducesResponseType(typeof(SurveyVie>), 200)]
    public Result GetSurveyStats([FromQuery] SurveyStatsFilters filters)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("surveys/add")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    // [ProducesResponseType(typeof(SurveyVie>), 200)]
    public Result AddAccessToSurveys([FromQuery] string accessKey)
    {
        throw new NotImplementedException();
    }
}