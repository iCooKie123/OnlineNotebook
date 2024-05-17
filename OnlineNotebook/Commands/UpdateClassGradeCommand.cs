using MediatR;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Commands
{
    public class UpdateClassGradeCommand : IRequest
    {
        public IEnumerable<UpdateSingleStudentGradeRequest> StudentGrades { get; set; }
        public int ClassId { get; set; }
    }

    public class UpdateSingleStudentGradeRequest : IRequest
    {
        public int StudentId { get; set; }
        public int Grade { get; set; }
    }

    public class UpdateStudentGradeCommandHandler : IRequestHandler<UpdateClassGradeCommand>
    {
        private readonly IStudentClassService _studentClassService;
        private readonly DatabaseContext _dbContext;

        public UpdateStudentGradeCommandHandler(
            IStudentClassService studentClassService,
            DatabaseContext dbContext
        )
        {
            _studentClassService = studentClassService;
            _dbContext = dbContext;
        }

        public async Task Handle(
            UpdateClassGradeCommand request,
            CancellationToken cancellationToken
        )
        {
            foreach (var studentGrade in request.StudentGrades)
            {
                await _studentClassService.UpdateStudentClassGrade(
                    studentGrade.StudentId,
                    request.ClassId,
                    studentGrade.Grade,
                    cancellationToken
                );
            }
            _dbContext.SaveChanges();
        }
    }
}
