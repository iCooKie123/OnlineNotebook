using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.Services.Abstractions
{
    public interface IStudentClassService
    {
        Task<IEnumerable<StudentClass>> GetStudentClassAsync(int studentId);
    }
}