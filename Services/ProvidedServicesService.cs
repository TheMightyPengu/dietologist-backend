using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;

namespace dietologist_backend.Services
{
    public interface IProvidedServicesService
    {
        Task<IEnumerable<ProvidedServicesBaseDto>> GetAllDtosAsync();
        Task<ProvidedServicesBaseDto> GetDtoByIdAsync(int id);
        Task<ProvidedServicesBaseDto> AddAsync(ProvidedServicesBaseDto dto);
        Task UpdateAsync(int id, ProvidedServicesBaseDto dto);
        Task DeleteAsync(int id);
    }

    public class ProvidedServicesService : IProvidedServicesService
    {
        private readonly IProvidedServicesRepository _repository;
        private readonly IMapper _mapper;

        public ProvidedServicesService(IProvidedServicesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProvidedServicesBaseDto>> GetAllDtosAsync()
        {
            var services = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProvidedServicesBaseDto>>(services);
        }

        public async Task<ProvidedServicesBaseDto> GetDtoByIdAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);
            if (service == null)
            {
                throw new KeyNotFoundException($"ProvidedService with ID {id} not found.");
            }
            return _mapper.Map<ProvidedServicesBaseDto>(service);
        }

        public async Task<ProvidedServicesBaseDto> AddAsync(ProvidedServicesBaseDto dto)
        {
            var entity = _mapper.Map<ProvidedServices>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<ProvidedServicesBaseDto >(createdEntity);
        }

        public async Task UpdateAsync(int id, ProvidedServicesBaseDto dto)
        {
            var existingService = await _repository.GetByIdAsync(id);
            if (existingService == null)
            {
                throw new KeyNotFoundException($"ProvidedService with ID {id} not found.");
            }

            var updatedService = _mapper.Map(dto, existingService);
            await _repository.UpdateAsync(updatedService);
        }

        public async Task DeleteAsync(int id)
        {
            var existingService = await _repository.GetByIdAsync(id);
            if (existingService == null)
            {
                throw new KeyNotFoundException($"ProvidedService with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}