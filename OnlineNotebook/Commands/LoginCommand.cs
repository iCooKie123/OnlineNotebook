using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using OnlineNotebook.Controllers.CustomExceptions;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Commands
{
    public class LoginCommand : IRequest<string>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        public IUserService _userService { get; set; }
        public IConfiguration _configuration { get; set; }
        public IMapper _mapper { get; set; }
        private JsonSerializerOptions Options { get; set; }

        public LoginCommandHandler(
            IUserService userService,
            IConfiguration configuration,
            IMapper mapper
        )
        {
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;

            Options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request.Email == null || request.Password == null)
            {
                throw new ForbiddenException("Email or password is not present.");
            }

            var user = await _userService.Login(request.Email, request.Password);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("User", JsonSerializer.Serialize(user, Options)),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!);
            var expiryMinutes = DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration["JwtSettings:ExpirationHours"]!) * 60
            );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiryMinutes,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string userToken = tokenHandler.WriteToken(token);

            return userToken;
        }
    }
}
