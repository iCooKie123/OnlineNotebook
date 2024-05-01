using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.Queries;

namespace OnlineNotebook.Services.Abstractions
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetUsers();

        public Task<UserDTO> GetUserByEmailAsync(string email);

        public void UpdateUser(User user);

        public void AddUser(User user);

        public Task<UserDTO> Login(string email, string password);
        public Task<string> UpdateUserPassword(string oldPassword, string newPassword, int? userId);
    }
}
