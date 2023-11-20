using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backoffice.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Produces("application/json")]
// [ProducesErrorResponseType(typeof(Astrum.Core.Common.Results.Result))]
[ProducesResponseType(typeof(UnprocessableEntityResult), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ForbidResult), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public abstract class ApiBaseController: ControllerBase
{
    private ILogger? _logger;
    private IMapper? _mapper;
    // private ISender? _mediator;
}