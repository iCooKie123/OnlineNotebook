using MediatR;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Commands
{
    public class UpdateClassGradeCommand : IRequest
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public int Grade { get; set; }
    }

    public class UpdateStudentGradeCommandHandler : IRequestHandler<UpdateClassGradeCommand>
    {
        private readonly IStudentClassService _studentClassService;

        public UpdateStudentGradeCommandHandler(IStudentClassService studentClassService)
        {
            _studentClassService = studentClassService;
        }

        public async Task Handle(
            UpdateClassGradeCommand request,
            CancellationToken cancellationToken
        ) =>
            await _studentClassService.UpdateStudentClassGrade(
                request.StudentId,
                request.ClassId,
                request.Grade,
                cancellationToken
            );
    }
}
