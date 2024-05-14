using AutoMapper;
using MediatR;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Queries
{
    public class GetStudentClassQuery : IRequest<IEnumerable<GetStudentClassQueryResponse>>
    {
        public int ClassId { get; private set; }

        public GetStudentClassQuery WithClassId(int classId)
        {
            ClassId = classId;
            return this;
        }
    }

    public class GetStudentClassQueryResponse
    {
        public int Grade { get; set; }
        public User Student { get; set; }
        public int ClassId { get; set; }
    }

    public class GetStudentClassHandler
        : IRequestHandler<GetStudentClassQuery, IEnumerable<GetStudentClassQueryResponse>>
    {
        private readonly IStudentClassService _studentClassService;
        private readonly IMapper _mapper;

        public GetStudentClassHandler(IStudentClassService studentClassService, IMapper mapper)
        {
            _studentClassService = studentClassService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetStudentClassQueryResponse>> Handle(
            GetStudentClassQuery request,
            CancellationToken cancellationToken
        )
        {
            var studentClasses = await _studentClassService.GetStudentClassesAsync(
                request.ClassId,
                cancellationToken
            );

            return _mapper.Map<IEnumerable<GetStudentClassQueryResponse>>(studentClasses);
        }
    }
}
