using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dietologist_backend.Services
{
    public interface IAppointmentsService
    {
        Task<IEnumerable<AppointmentsBaseDto>> GetAllDtosAsync();
        Task<AppointmentsBaseDto> GetDtoByIdAsync(int id);
        Task<AppointmentsBaseDto> AddAsync(AppointmentsBaseDto dto);
        Task UpdateAsync(int id, AppointmentsBaseDto dto);
        Task DeleteAsync(int id);
    }

    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentsRepository _repository;
        private readonly IMapper _mapper;

        public AppointmentsService(IAppointmentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentsBaseDto>> GetAllDtosAsync()
        {
            var appointments = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AppointmentsBaseDto>>(appointments);
        }

        public async Task<AppointmentsBaseDto> GetDtoByIdAsync(int id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            return _mapper.Map<AppointmentsBaseDto>(appointment);
        }

        public async Task<AppointmentsBaseDto> AddAsync(AppointmentsBaseDto dto)
        {
            var entity = _mapper.Map<Appointments>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<AppointmentsBaseDto>(createdEntity);
        }

        public async Task UpdateAsync(int id, AppointmentsBaseDto dto)
        {
            var existingAppointment = await _repository.GetByIdAsync(id);
            if (existingAppointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }

            var updatedAppointment = _mapper.Map(dto, existingAppointment);
            await _repository.UpdateAsync(updatedAppointment);
        }

        public async Task DeleteAsync(int id)
        {
            var existingAppointment = await _repository.GetByIdAsync(id);
            if (existingAppointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}