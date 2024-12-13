using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services;

public interface IResumeService
{
    Task<IEnumerable<ResumeBaseDto>> GetAllDtosAsync();
    Task<ResumeBaseDto?> GetDtoByIdAsync(int id);
    Task<ResumeBaseDto> AddAsync(ResumeBaseDto dto);
    Task UpdateAsync(int id, ResumeBaseDto dto);
    Task DeleteAsync(int id);
}

public class ResumeService : IResumeService
{
    private readonly IResumeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ResumeBaseDto> _validator;

    public ResumeService(
        IResumeRepository repository,
        IMapper mapper,
        IValidator<ResumeBaseDto> validator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<IEnumerable<ResumeBaseDto>> GetAllDtosAsync()
    {
        var resumes = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ResumeBaseDto>>(resumes);
    }

    public async Task<ResumeBaseDto?> GetDtoByIdAsync(int id)
    {
        var resume = await FindResumeByIdAsync(id);
        return _mapper.Map<ResumeBaseDto>(resume);
    }

    public async Task<ResumeBaseDto> AddAsync(ResumeBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<Resume>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<ResumeBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, ResumeBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingResume = await FindResumeByIdAsync(id);
        _mapper.Map(dto, existingResume);
        await _repository.UpdateAsync(existingResume);
    }

    public async Task DeleteAsync(int id)
    {
        var existingResume = await FindResumeByIdAsync(id);
        await _repository.DeleteAsync(existingResume.Id);
    }

    private async Task<Resume> FindResumeByIdAsync(int id)
    {
        var resume = await _repository.GetByIdAsync(id);
        if (resume == null)
            throw new KeyNotFoundException($"Resume with ID {id} not found.");

        return resume;
    }

    private async Task ValidateDtoAsync(ResumeBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException("Validation failed", validationResult.Errors);
        }
    }
}
