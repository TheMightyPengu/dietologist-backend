using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services;

public interface IImagesService
{
    Task<IEnumerable<ImagesBaseDto>> GetAllDtosAsync();
    Task<ImagesBaseDto?> GetDtoByIdAsync(int id);
    Task<ImagesBaseDto> AddAsync(ImagesBaseDto dto);
    Task UpdateAsync(int id, ImagesBaseDto dto);
    Task DeleteAsync(int id);
}

public class ImagesService(
    IImagesRepository repository,
    IMapper mapper,
    IValidator<ImagesBaseDto> validator)
    : IImagesService
{
    private readonly IImagesRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IValidator<ImagesBaseDto> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public async Task<IEnumerable<ImagesBaseDto>> GetAllDtosAsync()
    {
        var images = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ImagesBaseDto>>(images);
    }

    public async Task<ImagesBaseDto?> GetDtoByIdAsync(int id)
    {
        var image = await FindImageByIdAsync(id);
        return _mapper.Map<ImagesBaseDto>(image);
    }

    public async Task<ImagesBaseDto> AddAsync(ImagesBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<Images>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<ImagesBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, ImagesBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingImage = await FindImageByIdAsync(id);
        _mapper.Map(dto, existingImage);
        await _repository.UpdateAsync(existingImage);
    }

    public async Task DeleteAsync(int id)
    {
        var existingImage = await FindImageByIdAsync(id);
        await _repository.DeleteAsync(existingImage.Id);
    }

    private async Task<Images> FindImageByIdAsync(int id)
    {
        var image = await _repository.GetByIdAsync(id);
        if (image == null)
            throw new KeyNotFoundException($"Image with ID {id} not found.");

        return image;
    }

    private async Task ValidateDtoAsync(ImagesBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException("Validation failed", validationResult.Errors);
        }
    }
}
