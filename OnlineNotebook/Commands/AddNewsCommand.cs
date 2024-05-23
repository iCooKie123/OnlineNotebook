using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.Commands
{
    public class AddNewsCommand : IRequest
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
    }

    public class AddNewsCommandHandler : IRequestHandler<AddNewsCommand>
    {
        private readonly DatabaseContext _context;
        private readonly IMemoryCache _cache;

        public AddNewsCommandHandler(DatabaseContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task Handle(AddNewsCommand request, CancellationToken cancellationToken)
        {
            var newNews = new News(request.Title, request.Content);

            await _context.News.AddAsync(newNews, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _cache.Remove(CacheKeys.News);
        }
    }
}
