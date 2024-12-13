using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services;

public interface IRecipesService
{
    Task<IEnumerable<RecipesBaseDto>> GetAllDtosAsync();
    Task<RecipesBaseDto?> GetDtoByIdAsync(int id);
    Task<RecipesBaseDto> AddAsync(RecipesBaseDto dto);
    Task UpdateAsync(int id, RecipesBaseDto dto);
    Task DeleteAsync(int id);
}

public class RecipesService(
    IRecipesRepository repository,
    IMapper mapper,
    IValidator<RecipesBaseDto> validator)
    : IRecipesService
{
    private readonly IRecipesRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IValidator<RecipesBaseDto> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public async Task<IEnumerable<RecipesBaseDto>> GetAllDtosAsync()
    {
        var recipes = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<RecipesBaseDto>>(recipes);
    }

    public async Task<RecipesBaseDto?> GetDtoByIdAsync(int id)
    {
        var recipe = await FindRecipeByIdAsync(id);
        return _mapper.Map<RecipesBaseDto>(recipe);
    }

    public async Task<RecipesBaseDto> AddAsync(RecipesBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<Recipes>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<RecipesBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, RecipesBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingRecipe = await FindRecipeByIdAsync(id);
        _mapper.Map(dto, existingRecipe);
        await _repository.UpdateAsync(existingRecipe);
    }

    public async Task DeleteAsync(int id)
    {
        var existingRecipe = await FindRecipeByIdAsync(id);
        await _repository.DeleteAsync(existingRecipe.Id);
    }

    private async Task<Recipes> FindRecipeByIdAsync(int id)
    {
        var recipe = await _repository.GetByIdAsync(id);
        if (recipe == null)
            throw new KeyNotFoundException($"Recipe with ID {id} not found.");

        return recipe;
    }

    private async Task ValidateDtoAsync(RecipesBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException("Validation failed", validationResult.Errors);
        }
    }
}
