using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Repository;

public interface IUserRepository : IRepository<User>
{
    Task<List<User>> GetAllUsers();
    Task<User> GetUserWithStars(Guid userId);
    Task<User> GetUserByEmail(string email);
    Task AddUser(User user);
    Task DeleteUser(Guid userId);
}
