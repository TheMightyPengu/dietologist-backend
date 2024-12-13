using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services;

public interface ISocialMediaLinksService
{
    Task<IEnumerable<SocialMediaLinksBaseDto>> GetAllDtosAsync();
    Task<SocialMediaLinksBaseDto?> GetDtoByIdAsync(int id);
    Task<SocialMediaLinksBaseDto> AddAsync(SocialMediaLinksBaseDto dto);
    Task UpdateAsync(int id, SocialMediaLinksBaseDto dto);
    Task DeleteAsync(int id);
}

public class SocialMediaLinksService : ISocialMediaLinksService
{
    private readonly ISocialMediaLinksRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<SocialMediaLinksBaseDto> _validator;

    public SocialMediaLinksService(
        ISocialMediaLinksRepository repository,
        IMapper mapper,
        IValidator<SocialMediaLinksBaseDto> validator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<IEnumerable<SocialMediaLinksBaseDto>> GetAllDtosAsync()
    {
        var links = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<SocialMediaLinksBaseDto>>(links);
    }

    public async Task<SocialMediaLinksBaseDto?> GetDtoByIdAsync(int id)
    {
        var link = await FindLinkByIdAsync(id);
        return _mapper.Map<SocialMediaLinksBaseDto>(link);
    }

    public async Task<SocialMediaLinksBaseDto> AddAsync(SocialMediaLinksBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<SocialMediaLinks>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<SocialMediaLinksBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, SocialMediaLinksBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingLink = await FindLinkByIdAsync(id);
        _mapper.Map(dto, existingLink);
        await _repository.UpdateAsync(existingLink);
    }

    public async Task DeleteAsync(int id)
    {
        var existingLink = await FindLinkByIdAsync(id);
        await _repository.DeleteAsync(existingLink.Id);
    }

    private async Task<SocialMediaLinks> FindLinkByIdAsync(int id)
    {
        var link = await _repository.GetByIdAsync(id);
        if (link == null)
            throw new KeyNotFoundException($"SocialMediaLink with ID {id} not found.");

        return link;
    }

    private async Task ValidateDtoAsync(SocialMediaLinksBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException("Validation failed", validationResult.Errors);
        }
    }
}
