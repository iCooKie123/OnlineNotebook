using MediatR;
using OnlineNotebook.Services.Abstractions;

public class GetUsersQuery : IRequest<IEnumerable<UserDTO>>
{
}

public class UserDTO
{
    public string Email { get; set; }
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDTO>>
{
    private readonly IUserService _userService;

    public GetUsersQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IEnumerable<UserDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUsers();
    }
}