using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TheDevSpace.Application.Validation;
using TheDevSpace.Application.ValidationService;
using TheDevSpace.Domain.Entities;
using TheDevSpace.Repository.Repository;

namespace TheDevSpace.Application;

public class UserService : ServiceBase, IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<UserDto> _passwordHasher;

    public UserService(IValidationService validationService, IMapper mapper, IUserRepository userRepository, IPasswordHasher<UserDto> passwordHasher) : base(validationService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> AddUser(UserDto userDto)
    {
        if (userDto == null) throw new ArgumentNullException(nameof(userDto));
        if (!ExecuteValidation(new UserValidation(), userDto)) return null;

        if (await _userRepository.GetUserByEmail(userDto.Email) != null)
        {
            Notificate("This e-mail is in use. Choose another!");
            return null;
        }

        var passwordHash = _passwordHasher.HashPassword(userDto, userDto.Password);
        var user = new User(userDto.Email, passwordHash, userDto.Name);

        await _userRepository.AddUser(user);
        await _userRepository.UnitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUser(UserDto userDto)
    {
        if (userDto == null) throw new ArgumentNullException(nameof(userDto));
        if (!ExecuteValidation(new UserUpdateValidation(), userDto)) return null;

        var user = await _userRepository.GetUserWithWriter(userDto.UserId);

        if (!userDto.Password.IsNullOrEmpty())
        {
            var newUserHash = _passwordHasher.HashPassword(userDto, userDto.Password);

            if (_passwordHasher.VerifyHashedPassword(userDto, user.Password, newUserHash) == PasswordVerificationResult.Failed)
                user.ChangePassword(newUserHash);
        }

        user.UpdateName(userDto.Name);

        if (user.Writer != null)
        {
            user.Writer.UpdateData(userDto.Writer.Age, userDto.Writer.Role, userDto.Writer.Description);
        }

        await _userRepository.UnitOfWork.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUser(Guid userId)
    {
        await _userRepository.DeleteUser(userId);
        await _userRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();

        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> GetUser(Guid userId)
    {
        var user = await _userRepository.GetUserWithWriter(userId);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserWithStars(Guid userId)
    {
        var user = await _userRepository.GetUserWithStars(userId);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> LoginUser(UserDto userDto)
    {
        if (userDto == null) throw new ArgumentNullException(nameof(userDto));
        if (userDto.Email.IsNullOrEmpty()) throw new ArgumentNullException(nameof(userDto.Email));

        var user = await _userRepository.GetUserByEmail(userDto.Email);

        if (user == null)
        {
            Notificate("User or password invalid!");
            return null;
        }

        if (_passwordHasher.VerifyHashedPassword(userDto, user.Password, userDto.Password) == PasswordVerificationResult.Failed)
        {
            Notificate("User or password invalid!");
            return null;
        }

        return _mapper.Map<UserDto>(user);
    }
}
