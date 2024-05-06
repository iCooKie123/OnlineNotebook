using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.Services.Abstractions
{
    public interface IStudentClassService
    {
        Task<IEnumerable<StudentClass>> GetStudentClassAsync(int studentId);
        Task UpdateStudentClassGrade(
            int studentId,
            int classId,
            int? grade,
            CancellationToken cancellationToken
        );
    }
}
