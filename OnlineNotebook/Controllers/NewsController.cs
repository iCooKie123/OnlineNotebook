using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineNotebook.DatabaseConfigurations.Entities;
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
    }
}
