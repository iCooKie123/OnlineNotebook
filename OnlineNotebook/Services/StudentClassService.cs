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
        private IDatabaseContext _context;
        private IMapper _mapper;

        public StudentClassService(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentClass>> GetStudentClassAsync(int studentId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == studentId) ??
                throw new NotFoundException($"User with id ${studentId} was not found");

            var studentClasses = await _context.StudentClases
                .Include(s => s.Student)
                .Where(s => s.Student.Id == studentId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<StudentClass>>(studentClasses);
        }
    }
}