using DTOs;
using Microsoft.AspNetCore.Mvc;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Controllers
{
    [ApiController]
    [Route(Route)]
    public class UserController : ControllerBase
    {
        private const string Route = "users";
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/all")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }
    }
}