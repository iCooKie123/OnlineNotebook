using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineNotebook.Commands;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;
using OnlineNotebook.Queries;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Controllers
{
    [ApiController]
    [Route(Route)]
    public class UserController : ControllerBase
    {
        public const string Route = "users";
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        private JsonSerializerOptions _options;

        public UserController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;

            _options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [Authorize(Policy=PolicyName.RequireAdminRole)]
        [HttpGet(Name = nameof(GetUsers))]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers() =>
            Ok(await _mediator.Send(new GetUsersQuery()));

        [AllowAnonymous]
        [HttpPut("login", Name = nameof(Login))]
        public async Task<ActionResult<string>> Login([FromBody] LoginCommand request) =>
            Ok(await _mediator.Send(request));

        [Authorize(Policy = PolicyName.RequireAdminRole)]
        [HttpGet("validate-token", Name = nameof(ValidateToken))]
        public ActionResult ValidateToken() => Ok();

        [Authorize(Policy=PolicyName.RequireStudentRole)]
        [HttpPatch("change-password", Name = nameof(UpdateUserPasswordAsync))]
        public async Task<ActionResult<string>> UpdateUserPasswordAsync([FromBody] UpdateUserPasswordCommand request)
        {
            var userClaim = (User.FindFirst("User")?.Value) ?? throw new ForbiddenException("User claim was null");
            var userDeserialized = JsonSerializer.Deserialize<UserDTO>(userClaim, _options);
            var userId = userDeserialized.Id;

            return Ok(await _mediator.Send(request.WithId(userId)));
        }
    }
}