namespace TheDevSpace.Application;

public interface IWriterService
{
    Task<List<WriterDto>> GetAllWriters();
    Task AddWriter(WriterDto writerDto);
    Task DeleteWriter(Guid writerId);
    Task<WriterDto> GetWriterByName(string name);
    Task<List<WriterDto>> SearchWriterByName(string name);
    Task<WriterDto> GetWriterWithArticles(Guid writerId);
}
