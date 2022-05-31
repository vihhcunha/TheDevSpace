﻿namespace TheDevSpace.Application;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto> GetUserWithStars(Guid userId);
    Task<UserDto> GetUserByEmail(string email);
    Task AddUser(UserDto userDto);
    Task DeleteUser(Guid userId);
}