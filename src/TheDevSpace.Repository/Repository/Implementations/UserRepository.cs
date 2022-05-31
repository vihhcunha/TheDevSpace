using Microsoft.EntityFrameworkCore;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(TheDevSpaceContext context) : base(context)
    {
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task DeleteUser(Guid userId)
    {
        var user = await GetUserWithStars(userId);

        _context.Users.Remove(user);
        _context.ArticleStars.RemoveRange(user.StarredArticles);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetUserWithStars(Guid userId)
    {
        return await _context.Users
            .Include(u => u.StarredArticles)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }
}
