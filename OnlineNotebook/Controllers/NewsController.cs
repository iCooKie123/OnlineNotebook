using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineNotebook.Commands;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;
using OnlineNotebook.Queries;

namespace OnlineNotebook.Controllers
{
    [ApiController]
    [Route(Route)]
    public class NewsController : ControllerBase
    {
        public const string Route = "news";
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = nameof(GetAllNews))]
        public async Task<ActionResult<IEnumerable<News>>> GetAllNews() =>
            Ok(await _mediator.Send(new GetAllNewsQuery()));

        [HttpPatch("edit", Name = nameof(EditNewsOrAddNews))]
        [Authorize(Policy = PolicyName.RequireAdminRole)]
        public async Task<ActionResult> EditNewsOrAddNews([FromBody] EditNewsCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("add", Name = nameof(AddNews))]
        [Authorize(Policy = PolicyName.RequireAdminRole)]
        public async Task<ActionResult> AddNews([FromBody] AddNewsCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
