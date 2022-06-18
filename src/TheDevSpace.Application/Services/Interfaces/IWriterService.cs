namespace TheDevSpace.Application;

public interface IWriterService
{
    Task<List<WriterDto>> GetAllWriters();
    Task<WriterDto> AddWriter(WriterDto writerDto);
    Task DeleteWriter(Guid writerId);
    Task<WriterDto> GetWriterWithArticles(Guid writerId);
}
