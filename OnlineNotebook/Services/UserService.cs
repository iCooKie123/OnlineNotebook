using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.Queries;
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

        public Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
            return user != null
                ? Task.FromResult(_mapper.Map<UserDTO>(user))
                : throw new NotFoundException("user not found");
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> Login(string email, string password)
        {
            var user = await _dbContext
                .Users.Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();

            return user == null
                ? throw new ForbiddenException("The email or the password was incorrect")
                : _mapper.Map<UserDTO>(user);
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateUserPassword(string oldPassword, string newPassword, int? userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x =>
                x.Id == userId && x.Password == oldPassword
            ) ?? throw new ForbiddenException("The old password was incorrect");

            user.UpdatePassword(newPassword);
            _dbContext.SaveChanges();

            return Task.FromResult("Password successfuly changed");
        }
    }
}