using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineNotebook.Commands;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;
using OnlineNotebook.Queries;

namespace OnlineNotebook.Controllers
{
    [ApiController]
    [Route(Route)]
    public class ClassesController : ControllerBase
    {
        public const string Route = "classes";
        private readonly IMediator _mediator;
        private readonly JsonSerializerOptions _options;

        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;

            _options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        [Authorize(Policy = PolicyName.RequireStudentRole)]
        [HttpGet(Name = nameof(GetAllStudentClasses))]
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

        [Authorize(Policy = PolicyName.RequireAdminRole)]
        [HttpGet("student/{studentId}", Name = nameof(GettAllStudentClassesForStudent))]
        public async Task<
            ActionResult<IEnumerable<GetStudentClassesQueryResponse>>
        > GettAllStudentClassesForStudent(int studentId)
        {
            return Ok(await _mediator.Send(new GetStudentClassesQuery().WithStudentId(studentId)));
        }

        [Authorize(Policy = PolicyName.RequireAdminRole)]
        [HttpGet("all-classes", Name = nameof(GetAllStudyClasses))]
        public async Task<
            ActionResult<IEnumerable<IEnumerable<StudyClass>>>
        > GetAllStudyClasses() => Ok(await _mediator.Send(new GetAllClassesQuery()));

        [Authorize(Policy = PolicyName.RequireAdminRole)]
        [HttpGet("class/{classId}", Name = nameof(GetStudentClassById))]
        public async Task<ActionResult<IEnumerable<StudentClass>>> GetStudentClassById(
            int classId
        ) => Ok(await _mediator.Send(new GetStudentClassQuery().WithClassId(classId)));

        [Authorize(Policy = PolicyName.RequireAdminRole)]
        [HttpPatch("{classId}/grades", Name = nameof(UpdateClassGrades))]
        public async Task<ActionResult> UpdateClassGrades(
            int classId,
            [FromBody] UpdateClassGradeCommand command
        )
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
