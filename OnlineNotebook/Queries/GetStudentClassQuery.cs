using AutoMapper;
using MediatR;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Queries
{
    public class GetStudentClassQuery : IRequest<IEnumerable<StudentClass>>
    {
        public int ClassId { get; private set; }

        public GetStudentClassQuery WithClassId(int classId)
        {
            ClassId = classId;
            return this;
        }
    }

    public class GetStudentClassHandler
        : IRequestHandler<GetStudentClassQuery, IEnumerable<StudentClass>>
    {
        private readonly IStudentClassService _studentClassService;
        private readonly IMapper _mapper;

        public GetStudentClassHandler(IStudentClassService studentClassService, IMapper mapper)
        {
            _studentClassService = studentClassService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentClass>> Handle(
            GetStudentClassQuery request,
            CancellationToken cancellationToken
        ) => await _studentClassService.GetStudentClassesAsync(request.ClassId, cancellationToken);
    }
}
