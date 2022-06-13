using AutoMapper;
using TheDevSpace.Application.Validation;
using TheDevSpace.Application.ValidationService;
using TheDevSpace.Domain.Entities;
using TheDevSpace.Repository.Repository;

namespace TheDevSpace.Application;

public class WriterService : ServiceBase, IWriterService
{
    private readonly IMapper _mapper;
    private readonly IWriterRepository _writerRepository;

    public WriterService(IValidationService validationService, IMapper mapper, IWriterRepository writerRepository) : base(validationService)
    {
        _mapper = mapper;
        _writerRepository = writerRepository;
    }

    public async Task AddWriter(WriterDto writerDto)
    {
        if (writerDto == null) throw new ArgumentNullException(nameof(writerDto));
        if (!ExecuteValidation(new WriterValidation(), writerDto)) return;

        var writer = new Writer(writerDto.Age, writerDto.Description, writerDto.Role, writerDto.UserId);
        await _writerRepository.AddWriter(writer);
        await _writerRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task DeleteWriter(Guid writerId)
    {
        var writer = await _writerRepository.GetWriterWithArticles(writerId);

        if ((bool)(writer?.Articles?.Any())) 
        { 
            Notificate("It's not possible delete this writer, because he/she wrote some articles.");
            return;
        } 

        await _writerRepository.DeleteWriter(writer);
        await _writerRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task<List<WriterDto>> GetAllWriters()
    {
        var writers = await _writerRepository.GetAllWriters();

        return _mapper.Map<List<WriterDto>>(writers);
    }

    public async Task<WriterDto> GetWriterWithArticles(Guid writerId)
    {
        var writer = await _writerRepository.GetWriterWithArticles(writerId);

        return _mapper.Map<WriterDto>(writer);
    }
}
