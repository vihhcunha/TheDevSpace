namespace TheDevSpace.Repository;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    void SaveChanges();
}
