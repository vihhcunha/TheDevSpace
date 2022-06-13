namespace TheDevSpace.Application;

public interface IWriterService
{
    Task<List<WriterDto>> GetAllWriters();
    Task AddWriter(WriterDto writerDto);
    Task DeleteWriter(Guid writerId);
    Task<WriterDto> GetWriterWithArticles(Guid writerId);
}
