using AutoMapper;
using DTOs;
using Microsoft.EntityFrameworkCore;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DTOs;
using OnlineNotebook.Services.Abstractions;
using System.Linq;

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

        public async Task<LoginDTO> Login(string email, string password)
        {
            var user = await _dbContext.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ForbiddenException("The email or the password was incorrect");
            }

            return new LoginDTO()
            {
                Email = user.Email,
                FirstName = "test",
                LastName = "test",
                Role = "test"
            };
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}