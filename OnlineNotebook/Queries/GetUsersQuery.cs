using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.Services.Abstractions;

public class GetUsersQuery
{
    private readonly IUserService _userService;
    public GetUsersQuery(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<IEnumerable<UserDTO>> Execute()
    {
        return await _userService.GetUsers();
    }
}
