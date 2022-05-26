using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Repository;

public interface IRepository<T> where T : Entity
{
    IUnitOfWork UnitOfWork { get; }
    Task<T> GetById(Guid id);
}
