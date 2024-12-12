using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services;

public interface IAppointmentsService
{
    Task<IEnumerable<AppointmentsBaseDto>> GetAllDtosAsync();
    Task<AppointmentsBaseDto?> GetDtoByIdAsync(int id);
    Task<AppointmentsBaseDto> AddAsync(AppointmentsBaseDto dto);
    Task UpdateAsync(int id, AppointmentsBaseDto dto);
    Task DeleteAsync(int id);
}

public class AppointmentsService : IAppointmentsService
{
    private readonly IAppointmentsRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<AppointmentsBaseDto> _validator;

    public AppointmentsService(
        IAppointmentsRepository repository, 
        IMapper mapper, 
        IValidator<AppointmentsBaseDto> validator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<IEnumerable<AppointmentsBaseDto>> GetAllDtosAsync()
    {
        var appointments = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<AppointmentsBaseDto>>(appointments);
    }

    public async Task<AppointmentsBaseDto?> GetDtoByIdAsync(int id)
    {
        var appointment = await FindAppointmentByIdAsync(id);
        return _mapper.Map<AppointmentsBaseDto>(appointment);
    }

    public async Task<AppointmentsBaseDto> AddAsync(AppointmentsBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var entity = _mapper.Map<Appointments>(dto);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<AppointmentsBaseDto>(createdEntity);
    }

    public async Task UpdateAsync(int id, AppointmentsBaseDto dto)
    {
        await ValidateDtoAsync(dto);

        var existingAppointment = await FindAppointmentByIdAsync(id);
        _mapper.Map(dto, existingAppointment);
        await _repository.UpdateAsync(existingAppointment);
    }

    public async Task DeleteAsync(int id)
    {
        var existingAppointment = await FindAppointmentByIdAsync(id);
        await _repository.DeleteAsync(existingAppointment.Id);
    }

    private async Task<Appointments> FindAppointmentByIdAsync(int id)
    {
        var appointment = await _repository.GetByIdAsync(id);
        if (appointment == null)
            throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            
        return appointment;
    }

    private async Task ValidateDtoAsync(AppointmentsBaseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            throw new ValidationException("Validation failed", validationResult.Errors);
    }
}