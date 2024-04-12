using AutoMapper;
using DTOs;
using Microsoft.EntityFrameworkCore;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DTOs;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Services
{
    public class UserService : IUserService
    {
        public DatabaseContext _dbContext;
        public IMapper _mapper;

        public UserService(DatabaseContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public Task<LoginDTO> Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}