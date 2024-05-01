using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.Queries;

namespace OnlineNotebook.Controllers
{
    [ApiController]
    [Route(Route)]
    public class ClassesController : ControllerBase
    {
        public const string Route = "classes";
        private readonly IMediator _mediator;
        private JsonSerializerOptions _options;

        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;

            _options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [Authorize]
        [HttpGet("all", Name = nameof(GetAllStudentClasses))]
        public async Task<
            ActionResult<IEnumerable<GetStudentClassesQueryResponse>>
        > GetAllStudentClasses()
        {
            var userClaim = User.FindFirst("User")?.Value;
            var userDeserialized = JsonSerializer.Deserialize<UserDTO>(userClaim, _options);
            if (userDeserialized.Id == null)
            {
                throw new ForbiddenException("Student Id was null");
            }
            var userId = userDeserialized.Id;

            return Ok(await _mediator.Send(new GetStudentClassesQuery().WithStudentId(userId)));
        }
    }
}
