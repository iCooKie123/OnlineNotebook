using DTOs;
using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.Services.Abstractions
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetUsers();

        public User GetUserByEmail(string email);

        public void UpdateUser(User user);

        public void AddUser(User user);
    }
}