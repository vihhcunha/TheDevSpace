using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Repository;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly TheDevSpaceContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public Repository(TheDevSpaceContext context)
    {
        _context = context;
    }

    public async Task<T> GetById(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
}
