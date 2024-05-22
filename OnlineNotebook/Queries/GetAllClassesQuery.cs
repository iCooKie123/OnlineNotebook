using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.Queries
{
    public class GetAllClassesQuery : IRequest<IEnumerable<StudyClass>> { }

    public class GetAllClassesQueryHandler
        : IRequestHandler<GetAllClassesQuery, IEnumerable<StudyClass>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetAllClassesQueryHandler(
            DatabaseContext context,
            IMapper mapper,
            IMemoryCache cache
        )
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<StudyClass>> Handle(
            GetAllClassesQuery request,
            CancellationToken cancellationToken
        )
        {
            if (_cache.TryGetValue<IEnumerable<StudyClass>>(CacheKeys.Classes, out var classes))
            {
                return classes;
            }

            var classesDb = await _context.Classes.ToListAsync(
                cancellationToken: cancellationToken
            );
            _cache.Set(CacheKeys.Classes, classesDb, TimeSpan.FromDays(1));
            return classesDb;
        }
    }
}
