using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Repository;

public interface IWriterRepository : IRepository<Writer>
{
    Task<List<Writer>> GetAllWriters();
    Task AddWriter(Writer writer);
    Task DeleteWriter(Guid writerId);
    Task DeleteWriter(Writer writer);
    Task<Writer> GetWriterWithArticles(Guid writerId);
}
