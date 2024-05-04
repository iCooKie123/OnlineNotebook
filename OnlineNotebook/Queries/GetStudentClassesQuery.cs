using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.Queries
{
    public class GetStudentClassesQuery : IRequest<IEnumerable<GetStudentClassesQueryResponse>>
    {
        public int StudentId { get; private set; }

        public GetStudentClassesQuery WithStudentId(int studentId)
        {
            StudentId = studentId;

            return this;
        }
    }

    public class GetStudentClassesQueryResponse
    {
        public StudyClass Class { get; set; }
        public int? Grade { get; set; }
    }

    internal class GetStudentClassesQueryHandler : IRequestHandler<GetStudentClassesQuery, IEnumerable<GetStudentClassesQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseContext _context;

        public GetStudentClassesQueryHandler(IMapper mapper, IDatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<GetStudentClassesQueryResponse>> Handle(GetStudentClassesQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.StudentId, cancellationToken: cancellationToken) ??
                throw new NotFoundException($"User with id ${request.StudentId} was not found");

            var studentClasses = await _context.StudentClases
                .Include(s => s.Student)
                .Include(s => s.Class)
                .Where(s => s.Student.Id == request.StudentId)
                .ToListAsync(cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<GetStudentClassesQueryResponse>>(studentClasses);
        }
    }
}