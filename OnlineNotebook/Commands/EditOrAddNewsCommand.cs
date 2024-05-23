using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.Commands
{
    public class EditOrAddNewsCommand : IRequest
    {
        public IEnumerable<News> News { get; set; }
    }

    public class EditOrAddNewsCommandHandler : IRequestHandler<EditOrAddNewsCommand>
    {
        private readonly DatabaseContext _context;
        private readonly IMemoryCache _cache;

        public EditOrAddNewsCommandHandler(DatabaseContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task Handle(EditOrAddNewsCommand request, CancellationToken cancellationToken)
        {
            foreach (var news in request.News)
            {
                if (news.Id == -1)
                {
                    var newNews = new News(news.Title, news.Content);
                    _context.News.Add(newNews);
                    continue;
                }

                var newsToUpdate =
                    await _context.News.FirstOrDefaultAsync(n => n.Id == news.Id)
                    ?? throw new NotFoundException($"News with id {news.Id} not found");

                newsToUpdate.UpdateTitle(news.Title);
                newsToUpdate.UpdateContent(news.Content);
                newsToUpdate.UpdateModifiedAt();
            }

            await _context.SaveChangesAsync(cancellationToken);
            _cache.Remove(CacheKeys.News);
        }
    }
}
