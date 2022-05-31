﻿using AutoMapper;
using TheDevSpace.Application.Validation;
using TheDevSpace.Application.ValidationService;
using TheDevSpace.Domain.Entities;
using TheDevSpace.Repository;
using TheDevSpace.Repository.Repository;

namespace TheDevSpace.Application;

public class WriterService : ServiceBase, IWriterService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWriterRepository _writerRepository;

    protected WriterService(IValidationService validationService, IMapper mapper, IUnitOfWork unitOfWork, IWriterRepository writerRepository) : base(validationService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _writerRepository = writerRepository;
    }

    public async Task AddWriter(WriterDto writerDto)
    {
        if (writerDto == null) throw new ArgumentNullException(nameof(writerDto));
        if (!ExecuteValidation(new WriterValidation(), writerDto)) return;

        var writer = new Writer(writerDto.Name, writerDto.Age, writerDto.Description, writerDto.Role, writerDto.UserId);
        await _writerRepository.AddWriter(writer);
        await _unitOfWork.SaveChangesAsync();
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
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<WriterDto>> GetAllWriters()
    {
        var writers = await _writerRepository.GetAllWriters();

        return _mapper.Map<List<WriterDto>>(writers);
    }

    public async Task<WriterDto> GetWriterByName(string name)
    {
        var writer = await _writerRepository.GetWriterByName(name);

        return _mapper.Map<WriterDto>(writer);
    }

    public async Task<WriterDto> GetWriterWithArticles(Guid writerId)
    {
        var writer = await _writerRepository.GetWriterWithArticles(writerId);

        return _mapper.Map<WriterDto>(writer);
    }

    public async Task<List<WriterDto>> SearchWriterByName(string name)
    {
        var writers = await _writerRepository.SearchWriterByName(name);

        return _mapper.Map<List<WriterDto>>(writers);
    }
}
