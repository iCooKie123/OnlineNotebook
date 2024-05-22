using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.Queries
{
    public class GetAllNewsQuery : IRequest<IEnumerable<News>> { }

    public class GetAllNewsGueryHandler : IRequestHandler<GetAllNewsQuery, IEnumerable<News>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetAllNewsGueryHandler(DatabaseContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<News>> Handle(
            GetAllNewsQuery request,
            CancellationToken cancellationToken
        )
        {
            if (_cache.TryGetValue<IEnumerable<News>>(CacheKeys.News, out var news))
            {
                return news;
            }

            news = await _context.News.ToListAsync(cancellationToken: cancellationToken);
            _cache.Set(CacheKeys.News, news, TimeSpan.FromDays(1));

            return news;
        }
    }
}
