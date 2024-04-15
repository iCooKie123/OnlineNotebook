using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineNotebook.Commands;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Controllers
{
    [ApiController]
    [Route(Route)]
    public class UserController : ControllerBase
    {
        private const string Route = "users";
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UserController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet(Name = nameof(GetUsers))]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers() => Ok(await _mediator.Send(new GetUsersQuery()));

        [AllowAnonymous]
        [HttpPut("login", Name = nameof(Login))]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginCommand request) => Ok(await _mediator.Send(request));

        [Authorize]
        [HttpGet("validate-token", Name = nameof(ValidateToken))]
        public ActionResult ValidateToken() => Ok();
    }
}