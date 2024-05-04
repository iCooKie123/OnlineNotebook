using MediatR;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Commands;

public class UpdateUserPasswordCommand : IRequest<string>
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public int? _userId;

    public UpdateUserPasswordCommand WithId(int userId)
    {
        _userId = userId;
        return this;
    }
}

public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, string>
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public UpdateUserPasswordHandler(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<string> Handle(
        UpdateUserPasswordCommand request,
        CancellationToken cancellationToken
    ) =>
         await _userService.UpdateUserPassword(
            request.OldPassword,
            request.NewPassword,
            request._userId
        );
}