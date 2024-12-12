using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services;

public interface IContactInfoService
{
    Task<IEnumerable<ContactInfoBaseDto>> GetAllDtosAsync();
    Task<ContactInfoBaseDto?> GetDtoByIdAsync(int id);
    Task<ContactInfoBaseDto> AddAsync(ContactInfoBaseDto dto);
    Task UpdateAsync(int id, ContactInfoBaseDto dto);
    Task DeleteAsync(int id);
}

public class ContactInfoService : IContactInfoService
{
    private readonly IContactInfoRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ContactInfoBaseDto> _validator;

    public ContactInfoService(
        IContactInfoRepository repository, 
        IMapper mapper, 
        IValidator<ContactInfoBaseDto> validator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<IEnumerable<ContactInfoBaseDto>> GetAllDtosAsync()
    {
        var contactInfos = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ContactInfoBaseDto>>(contactInfos);
    }

    public async Task<ContactInfoBaseDto?> GetDtoByIdAsync(int id)
    {
        var contactInfo = await FindContactInfoByIdAsync(id);
        return _mapper.Map<ContactInfoBaseDto>(contactInfo);
    }

    public async Task<ContactInfoBaseDto> AddAsync(ContactInfoBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<ContactInfo>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<ContactInfoBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, ContactInfoBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingContactInfo = await FindContactInfoByIdAsync(id);
        _mapper.Map(dto, existingContactInfo);
        await _repository.UpdateAsync(existingContactInfo);
    }

    public async Task DeleteAsync(int id)
    {
        var existingContactInfo = await FindContactInfoByIdAsync(id);
        await _repository.DeleteAsync(existingContactInfo.Id);
    }

    private async Task<ContactInfo> FindContactInfoByIdAsync(int id)
    {
        var contactInfo = await _repository.GetByIdAsync(id);
        if (contactInfo == null)
            throw new KeyNotFoundException($"ContactInfo with ID {id} not found.");
            
        return contactInfo;
    }

    private async Task ValidateDtoAsync(ContactInfoBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            throw new ValidationException("Validation failed", validationResult.Errors);
    }
}