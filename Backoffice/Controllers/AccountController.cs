using Application.Common.Result;
using Application.Models.Requests;
using Application.Models.Requests.Account;
using Application.Models.Responses;
using Application.Models.Responses.Account;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Common.Logging;
using Infrastructure.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

[Route("[controller]")]
public class AccountController: ApiBaseController
{
    // TODO сделать другой интерфейс, наследующийся от этого
    private readonly IAccountService<User> _service;
    
    public AccountController(IAccountService<User> service, ICustomLogger logger, IMapper mapper): base(logger, mapper)
    {
        _service = service;
    }
    
    [HttpGet]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(AuthorizedModel), 200)]
    public async Task<Result<AuthorizedModel>> GetAccountInfo()
    {
        throw new NotImplementedException();
        // var result = await _service.Login(request);
        // if (result.)
        // return result;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(AuthorizedModel), 200)]
    public async Task<Result<AuthorizedModel>> LoginAsync([FromBody] LoginModel request)
    {
        var result = await _service.Login(request);
        // if (result.)
        return result;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<Result> RegisterAsync([FromBody] RegistrationModel request)
    {
        var result = await _service.Register(request);
        return result;
    }

    // TODO а как сделать совмество с фронтом
    [AllowAnonymous]
    [HttpPost("{confirmationToken}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<Result> VerifyEmailAsync([FromRoute] string confirmationToken, [FromQuery] Guid id)
    {
        var result = await _service.VerifyEmail(id, confirmationToken);
        return result;
    }
}