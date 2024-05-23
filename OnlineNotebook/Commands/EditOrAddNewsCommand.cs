using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.Commands
{
    public class EditNewsCommand : IRequest
    {
        public required IEnumerable<News> News { get; set; }
    }

    public class EditNewsCommandHandler : IRequestHandler<EditNewsCommand>
    {
        private readonly DatabaseContext _context;
        private readonly IMemoryCache _cache;

        public EditNewsCommandHandler(DatabaseContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task Handle(EditNewsCommand request, CancellationToken cancellationToken)
        {
            foreach (var news in request.News)
            {
                var newsToUpdate =
                    await _context.News.FirstOrDefaultAsync(
                        n => n.Id == news.Id,
                        cancellationToken: cancellationToken
                    ) ?? throw new NotFoundException($"News with id {news.Id} not found");

                newsToUpdate.UpdateTitle(news.Title);
                newsToUpdate.UpdateContent(news.Content);
                newsToUpdate.UpdateModifiedAt();
            }

            await _context.SaveChangesAsync(cancellationToken);
            _cache.Remove(CacheKeys.News);
        }
    }
}
