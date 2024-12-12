using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services;

public interface IProvidedServicesService
{
    Task<IEnumerable<ProvidedServicesBaseDto>> GetAllDtosAsync();
    Task<ProvidedServicesBaseDto?> GetDtoByIdAsync(int id);
    Task<ProvidedServicesBaseDto> AddAsync(ProvidedServicesBaseDto dto);
    Task UpdateAsync(int id, ProvidedServicesBaseDto dto);
    Task DeleteAsync(int id);
}

public class ProvidedServicesService(
    IProvidedServicesRepository repository,
    IMapper mapper,
    IValidator<ProvidedServicesBaseDto> validator)
    : IProvidedServicesService
{
    private readonly IProvidedServicesRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IValidator<ProvidedServicesBaseDto> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public async Task<IEnumerable<ProvidedServicesBaseDto>> GetAllDtosAsync()
    {
        var services = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProvidedServicesBaseDto>>(services);
    }

    public async Task<ProvidedServicesBaseDto?> GetDtoByIdAsync(int id)
    {
        var service = await FindServiceByIdAsync(id);
        return _mapper.Map<ProvidedServicesBaseDto>(service);
    }

    public async Task<ProvidedServicesBaseDto> AddAsync(ProvidedServicesBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<ProvidedServices>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<ProvidedServicesBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, ProvidedServicesBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingService = await FindServiceByIdAsync(id);
        _mapper.Map(dto, existingService); 
        await _repository.UpdateAsync(existingService);
    }

    public async Task DeleteAsync(int id)
    {
        var existingService = await FindServiceByIdAsync(id);
        await _repository.DeleteAsync(existingService.Id);
    }

    private async Task<ProvidedServices> FindServiceByIdAsync(int id)
    {
        var service = await _repository.GetByIdAsync(id);
        if (service == null)
            throw new KeyNotFoundException($"ProvidedService with ID {id} not found.");
            
        return service;
    }

    private async Task ValidateDtoAsync(ProvidedServicesBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException("Validation failed", validationResult.Errors);
        }
    }
}