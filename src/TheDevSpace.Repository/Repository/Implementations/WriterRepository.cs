using Microsoft.EntityFrameworkCore;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Repository;

public class WriterRepository : Repository<Writer>, IWriterRepository
{
    public WriterRepository(TheDevSpaceContext context) : base(context)
    {
    }

    public async Task AddWriter(Writer writer)
    {
        await _context.Writers.AddAsync(writer);
    }

    public async Task DeleteWriter(Guid writerId)
    {
        var writer = await GetById(writerId);

        _context.Writers.Remove(writer);
    }

    public async Task DeleteWriter(Writer writer)
    {
        _context.Writers.Remove(writer);
    }

    public async Task<List<Writer>> GetAllWriters()
    {
        return await _context.Writers
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<Writer> GetWriterWithArticles(Guid writerId)
    {
        return await _context.Writers
            .Include(w => w.Articles)
            .Include(w => w.User)
            .FirstOrDefaultAsync(w => w.WriterId == writerId);
    }
}
