using DTOs;
using Microsoft.AspNetCore.Mvc;
using OnlineNotebook.Services.Abstractions;
using System.Text.Json;

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

        [HttpGet(Name = nameof(GetUsers))]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var response = await _userService.GetUsers();
            return Ok(JsonSerializer.Serialize(response));
        }

        [HttpPut(Name = nameof(Login))]
        public async Task<ActionResult<UserDTO>> Login()
        {
            var response = await _userService.Login();
            return Ok(JsonSerializer.Serialize(response));
        }
    }
}