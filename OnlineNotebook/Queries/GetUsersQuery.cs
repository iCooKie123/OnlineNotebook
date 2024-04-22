using MediatR;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Queries;

public class GetUsersQuery : IRequest<IEnumerable<UserDTO>>
{
}

public class UserDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? LearningCycle { get; set; }
    public string? Faculty { get; set; }
    public string? Specialization { get; set; }
    public string? Group { get; set; }
    public int? YearOfStudy { get; set; }
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