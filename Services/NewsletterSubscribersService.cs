using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services;

public interface INewsletterSubscribersService
{
    Task<IEnumerable<NewsletterSubscribersBaseDto>> GetAllDtosAsync();
    Task<NewsletterSubscribersBaseDto?> GetDtoByIdAsync(int id);
    Task<NewsletterSubscribersBaseDto> AddAsync(NewsletterSubscribersBaseDto dto);
    Task UpdateAsync(int id, NewsletterSubscribersBaseDto dto);
    Task DeleteAsync(int id);
}

public class NewsletterSubscribersService(
    INewsletterSubscribersRepository repository,
    IMapper mapper,
    IValidator<NewsletterSubscribersBaseDto> validator)
    : INewsletterSubscribersService
{
    private readonly INewsletterSubscribersRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IValidator<NewsletterSubscribersBaseDto> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public async Task<IEnumerable<NewsletterSubscribersBaseDto>> GetAllDtosAsync()
    {
        var subscribers = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<NewsletterSubscribersBaseDto>>(subscribers);
    }

    public async Task<NewsletterSubscribersBaseDto?> GetDtoByIdAsync(int id)
    {
        var subscriber = await FindSubscriberByIdAsync(id);
        return _mapper.Map<NewsletterSubscribersBaseDto>(subscriber);
    }

    public async Task<NewsletterSubscribersBaseDto> AddAsync(NewsletterSubscribersBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<NewsletterSubscribers>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<NewsletterSubscribersBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, NewsletterSubscribersBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingSubscriber = await FindSubscriberByIdAsync(id);
        _mapper.Map(dto, existingSubscriber);
        await _repository.UpdateAsync(existingSubscriber);
    }

    public async Task DeleteAsync(int id)
    {
        var existingSubscriber = await FindSubscriberByIdAsync(id);
        await _repository.DeleteAsync(existingSubscriber.Id);
    }

    private async Task<NewsletterSubscribers> FindSubscriberByIdAsync(int id)
    {
        var subscriber = await _repository.GetByIdAsync(id);
        if (subscriber == null)
            throw new KeyNotFoundException($"NewsletterSubscriber with ID {id} not found.");

        return subscriber;
    }

    private async Task ValidateDtoAsync(NewsletterSubscribersBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException("Validation failed", validationResult.Errors);
        }
    }
}
