using Application.Models.Requests.Survey;
using Application.Models.Responses.Super;
using Application.Models.Responses.Survey;
using Application.Services.Super;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

[Authorize(Roles = nameof(RolesEnum.SuperAdmin))]
[Route("[controller]")]
public class SuperAdminController: ApiBaseController
{
    private readonly ISuperAdminService _service;
    
    public SuperAdminController(ICustomLogger logger, IMapper mapper, ISuperAdminService service) : base(logger, mapper)
    {
        _service = service;
    }
    
    [HttpGet("question/default")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(IEnumerable<QuestionView>), 200)]
    public Result<IEnumerable<QuestionView>> GetAllDefaultQuestion()
    {
        return Result.Success(_service.GetAllDefaultQuestions(AuthorizedUser.Id));
    }

    [HttpPost("question/default")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public Result CreateDefaultQuestion([FromBody] CreateQuestionRequest request)
    {
        _service.AddDefaultQuestion(request, AuthorizedUser.Id);
            
        return Result.Success();
    }
    
    [HttpDelete("question/default/{questionId:guid}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public Result CreateDefaultQuestion([FromRoute] Guid questionId)
    {
        _service.DeleteDefaultQuestion(AuthorizedUser.Id, questionId);
            
        return Result.Success();
    }
    
    [HttpGet("user")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(IEnumerable<UserView>), 200)]
    public Result<IEnumerable<UserView>> GetAllUsers([FromQuery] RolesEnum? role)
    {
        return Result.Success(_service.GetAllUsers(AuthorizedUser.Id));
    }
    
    [HttpDelete("user/{userId:guid}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public Result DeleteUser([FromRoute] Guid userId)
    {
        _service.DeleteUser(AuthorizedUser.Id, userId);
        
        return Result.Success();
    }
    
    [HttpGet("survey")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(IEnumerable<SuperSurveyView>), 200)]
    public Result<IEnumerable<SuperSurveyView>> GetAllSurveys()
    {
        return Result.Success(_service.GetAllSurveys(AuthorizedUser.Id));
    }
    
    [HttpDelete("survey/{surveyId:guid}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public Result DeleteSurvey([FromRoute] Guid surveyId)
    {
        _service.DeleteSurvey(AuthorizedUser.Id, surveyId);

        return Result.Success();
    }
    
    [HttpPost("database/clear")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public Result ClearDatabase([FromQuery] RolesEnum? role)
    {
        _service.ClearDatabase(AuthorizedUser.Id);
        
        return Result.Success();
    }
}