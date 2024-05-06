using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.Queries
{
    public class GetAllClassesQuery : IRequest<IEnumerable<StudyClass>> { }

    public class GetAllClassesQueryHandler
        : IRequestHandler<GetAllClassesQuery, IEnumerable<StudyClass>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetAllClassesQueryHandler(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudyClass>> Handle(
            GetAllClassesQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _context.Classes.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
