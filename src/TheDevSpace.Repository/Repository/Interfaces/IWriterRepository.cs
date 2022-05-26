using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Repository;

public interface IWriterRepository : IRepository<Writer>
{
    Task<List<Writer>> GetAllWriters();
    Task AddWriter(Writer writer);
    Task DeleteWriter(Guid writerId);
    Task<Writer> GetWriterByName(string name);
    Task<Writer> SearchWriterByName(string name);
    Task<Writer> GetWriterWithArticles(Guid writerId);
}
