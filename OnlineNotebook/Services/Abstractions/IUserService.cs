using OnlineNotebook.Commands;
using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.Services.Abstractions
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetUsers();

        public Task<UserDTO> GetUserByEmailAsync(string email);

        public void UpdateUser(User user);

        public void AddUser(User user);

        public Task<UserDTO> Login(string email, string password);
    }
}