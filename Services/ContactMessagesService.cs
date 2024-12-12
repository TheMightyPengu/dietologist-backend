using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services;

public interface IContactMessagesService
{
    Task<IEnumerable<ContactMessagesBaseDto>> GetAllDtosAsync();
    Task<ContactMessagesBaseDto?> GetDtoByIdAsync(int id);
    Task<ContactMessagesBaseDto> AddAsync(ContactMessagesBaseDto dto);
    Task UpdateAsync(int id, ContactMessagesBaseDto dto);
    Task DeleteAsync(int id);
}

public class ContactMessagesService : IContactMessagesService
{
    private readonly IContactMessagesRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ContactMessagesBaseDto> _validator;

    public ContactMessagesService(
        IContactMessagesRepository repository, 
        IMapper mapper, 
        IValidator<ContactMessagesBaseDto> validator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<IEnumerable<ContactMessagesBaseDto>> GetAllDtosAsync()
    {
        var messages = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ContactMessagesBaseDto>>(messages);
    }

    public async Task<ContactMessagesBaseDto?> GetDtoByIdAsync(int id)
    {
        var message = await FindMessageByIdAsync(id);
        return _mapper.Map<ContactMessagesBaseDto>(message);
    }

    public async Task<ContactMessagesBaseDto> AddAsync(ContactMessagesBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<ContactMessages>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<ContactMessagesBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, ContactMessagesBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingMessage = await FindMessageByIdAsync(id);
        _mapper.Map(dto, existingMessage);
        await _repository.UpdateAsync(existingMessage);
    }

    public async Task DeleteAsync(int id)
    {
        var existingMessage = await FindMessageByIdAsync(id);
        await _repository.DeleteAsync(existingMessage.Id);
    }

    private async Task<ContactMessages> FindMessageByIdAsync(int id)
    {
        var message = await _repository.GetByIdAsync(id);
        if (message == null)
            throw new KeyNotFoundException($"ContactMessage with ID {id} not found.");
        
        return message;
    }

    private async Task ValidateDtoAsync(ContactMessagesBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            throw new ValidationException("Validation failed", validationResult.Errors);
    }
}