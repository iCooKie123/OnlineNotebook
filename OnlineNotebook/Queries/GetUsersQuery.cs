using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Queries;

public class GetUsersQuery : IRequest<IEnumerable<UserDTO>> { }

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
    public UserRoles Role { get; set; }
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDTO>>
{
    private readonly IUserService _userService;
    private readonly IMemoryCache _cache;

    public GetUsersQueryHandler(IUserService userService, IMemoryCache cache)
    {
        _userService = userService;
        _cache = cache;
    }

    public async Task<IEnumerable<UserDTO>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        if (_cache.TryGetValue<IEnumerable<UserDTO>>(CacheKeys.Users, out var cacheUsers))
        {
            return cacheUsers;
        }

        var users = await _userService.GetUsers();
        _cache.Set(CacheKeys.Users, users, TimeSpan.FromDays(1));

        return users;
    }
}
