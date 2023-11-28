using System.Text.Json.Serialization;
using Application.Models.Responses.Account;
using AutoMapper;
using Infrastructure.Common.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backoffice.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Produces("application/json")]
[ProducesResponseType(typeof(UnprocessableEntityResult), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ForbidResult), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public abstract class ApiBaseController: ControllerBase
{
    protected readonly ICustomLogger _logger;
    protected readonly IMapper _mapper;
    // private ISender? _mediator;
    protected ApiBaseController(ICustomLogger logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    protected AuthorizedModel AuthorizedUser => AuthorizedModel.GetByClaimsWithoutToken(User);
}