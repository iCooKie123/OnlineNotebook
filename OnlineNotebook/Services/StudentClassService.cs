using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Services
{
    public class StudentClassService : IStudentClassService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public StudentClassService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentClass>> GetStudentClassAsync(int studentId)
        {
            var user =
                await _context.Users.FirstOrDefaultAsync(u => u.Id == studentId)
                ?? throw new NotFoundException($"User with id ${studentId} was not found");

            var studentClasses = await _context
                .StudentClases.Include(s => s.Student)
                .Where(s => s.Student.Id == studentId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StudentClass>>(studentClasses);
        }

        public async Task UpdateStudentClassGrade(
            int studentId,
            int classId,
            int? grade,
            CancellationToken c
        )
        {
            var studentClasses = await _context
                .StudentClases.Include(sc => sc.Student)
                .Include(sc => sc.Class)
                .ToListAsync(cancellationToken: c);

            var studentClass =
                studentClasses.FirstOrDefault(sc =>
                    sc.Student.Id == studentId && sc.Class.Id == classId
                )
                ?? throw new NotFoundException(
                    $"Class with id {classId} and student id {studentId} was not found"
                );

            studentClass.UpdateGrade(grade);
            await _context.SaveChangesAsync(c);
        }
    }
}
