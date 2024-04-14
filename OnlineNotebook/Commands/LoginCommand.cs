using MediatR;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Commands
{
    public class LoginCommand : IRequest<LoginDTO>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDTO>
    {
        public IUserService _userService { get; set; }

        public LoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request.Email == null || request.Password == null)
            {
                throw new ForbiddenException("Email or password is not present.");
            }

            return await _userService.Login(request.Email, request.Password);
        }
    }
}