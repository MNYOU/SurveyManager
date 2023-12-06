using Application.Common.Result;
using Application.Models.Requests;
using Application.Models.Requests.Account;
using Application.Models.Responses;
using Application.Models.Responses.Account;
using Application.Services;
using Application.Services.Account;
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
    private readonly IAccountService _service;
    
    public AccountController(IAccountService service, ICustomLogger logger, IMapper mapper): base(logger, mapper)
    {
        _service = service;
    }
    
    [HttpGet]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(AuthorizedModel), 200)]
    public Task<Result<AuthorizedModel>> GetAccountInfo()
    {
        var info = new AuthorizedModel()
        {
            Id = AuthorizedUser.Id,
            Email = AuthorizedUser.Email,
            Login = AuthorizedUser.Login,
            Role = AuthorizedUser.Role
        };
        return Task.FromResult<Result<AuthorizedModel>>(info);
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

    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<Result> RegisterAsync([FromBody] RegistrationModel request)
    {
        var result = await _service.RegisterAsync(request);
        return result;
    }

    // TODO а как сделать совмество с фронтом
    [AllowAnonymous]
    [HttpPost("{confirmationToken}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<Result> VerifyEmailAsync([FromRoute] string confirmationToken, [FromQuery] Guid id)
    {
        var result = await _service.VerifyEmailAsync(id, confirmationToken);
        return result;
    }

    [HttpDelete]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    public Task<Result> DeleteAccount()
    {
        throw new NotImplementedException();
    }
}