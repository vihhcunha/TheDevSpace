namespace TheDevSpace.Application;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto> GetUserWithStars(Guid userId);
    Task<UserDto> GetUser(Guid userId);
    Task<UserDto> GetUserByEmail(string email);
    Task<UserDto> AddUser(UserDto userDto);
    Task DeleteUser(Guid userId);
    Task<UserDto> LoginUser(UserDto userDto);
    Task<UserDto> UpdateUser(UserDto userDto);
}
