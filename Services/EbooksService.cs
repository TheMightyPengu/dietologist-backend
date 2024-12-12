using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

public interface IEbooksService
{
    Task<IEnumerable<EbooksBaseDto>> GetAllDtosAsync();
    Task<EbooksBaseDto?> GetDtoByIdAsync(int id);
    Task<EbooksBaseDto> AddAsync(EbooksBaseDto dto);
    Task UpdateAsync(int id, EbooksBaseDto dto);
    Task DeleteAsync(int id);
}

public class EbooksService : IEbooksService
{
    private readonly IEbooksRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<EbooksBaseDto> _validator;

    public EbooksService(
        IEbooksRepository repository, 
        IMapper mapper, 
        IValidator<EbooksBaseDto> validator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<IEnumerable<EbooksBaseDto>> GetAllDtosAsync()
    {
        var ebooks = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<EbooksBaseDto>>(ebooks);
    }

    public async Task<EbooksBaseDto?> GetDtoByIdAsync(int id)
    {
        var ebook = await FindEbookByIdAsync(id);
        return _mapper.Map<EbooksBaseDto>(ebook);
    }

    public async Task<EbooksBaseDto> AddAsync(EbooksBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<Ebooks>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<EbooksBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, EbooksBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingEbook = await FindEbookByIdAsync(id);
        _mapper.Map(dto, existingEbook);
        await _repository.UpdateAsync(existingEbook);
    }

    public async Task DeleteAsync(int id)
    {
        var existingEbook = await FindEbookByIdAsync(id);
        await _repository.DeleteAsync(existingEbook.Id);
    }

    private async Task<Ebooks> FindEbookByIdAsync(int id)
    {
        var ebook = await _repository.GetByIdAsync(id);
        if (ebook == null)
            throw new KeyNotFoundException($"Ebook with ID {id} not found.");
        
        return ebook;
    }

    private async Task ValidateDtoAsync(EbooksBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            throw new ValidationException("Validation failed", validationResult.Errors);
    }
}