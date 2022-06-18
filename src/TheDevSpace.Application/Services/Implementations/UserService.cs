using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TheDevSpace.Application.Validation;
using TheDevSpace.Application.ValidationService;
using TheDevSpace.Domain.Entities;
using TheDevSpace.Repository;
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
            Notificate("This e-mail is in use. Choose another!");

        var passwordHash = _passwordHasher.HashPassword(userDto, userDto.Password);

        User user;
        if (userDto.WriterId.HasValue)
        {
            user = new User(userDto.Email, passwordHash, userDto.WriterId.Value);
        }
        else
        {
            user = new User(userDto.Email, passwordHash, userDto.Name);
        }
        
        await _userRepository.AddUser(user);
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
