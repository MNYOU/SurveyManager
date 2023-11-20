using Application.Common.Result;
using Application.Models.Requests;
using Application.Models.Requests.Account;
using Application.Models.Responses;
using Application.Models.Responses.Account;
using Application.Services;
using Domain.Entities;
using Infrastructure.Common.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backoffice.Controllers;

[Route("[controller]")]
public class AccountController: ApiBaseController
{
    // TODO сделать другой интерфейс, наследующийся от этого
    private IAccountService<User> _service;
    
    public AccountController(IAccountService<User> service)
    {
        _service = service;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(Result<AuthorizedModel>), 200)]
    public async Task<Result<AuthorizedModel>> Login([FromBody] LoginModel request)
    {
        var result = await _service.Login(request);
        // if (result.)
        return result;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<Result> Register([FromBody] RegistrationModel request)
    {
        var result = await _service.Register(request);
        return result;
    }

    // TODO а как сделать совмество с фронтом
    [HttpGet("{confirmationToken}")]
    public async Task<Result> VerifyEmail([FromRoute] string confirmationToken, [FromQuery] Guid id)
    {
        var result = await _service.VerifyEmail(id, confirmationToken);
        return result;
    }
}