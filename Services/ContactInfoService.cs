using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dietologist_backend.Services
{
    public interface IContactInfoService
    {
        Task<IEnumerable<ContactInfoBaseDto>> GetAllDtosAsync();
        Task<ContactInfoBaseDto> GetDtoByIdAsync(int id);
        Task<ContactInfoBaseDto> AddAsync(ContactInfoBaseDto dto);
        Task UpdateAsync(int id, ContactInfoBaseDto dto);
        Task DeleteAsync(int id);
    }

    public class ContactInfoService : IContactInfoService
    {
        private readonly IContactInfoRepository _repository;
        private readonly IMapper _mapper;

        public ContactInfoService(IContactInfoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactInfoBaseDto>> GetAllDtosAsync()
        {
            var contactInfos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactInfoBaseDto>>(contactInfos);
        }

        public async Task<ContactInfoBaseDto> GetDtoByIdAsync(int id)
        {
            var contactInfo = await _repository.GetByIdAsync(id);
            if (contactInfo == null)
            {
                throw new KeyNotFoundException($"ContactInfo with ID {id} not found.");
            }
            return _mapper.Map<ContactInfoBaseDto>(contactInfo);
        }

        public async Task<ContactInfoBaseDto> AddAsync(ContactInfoBaseDto dto)
        {
            var entity = _mapper.Map<ContactInfo>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<ContactInfoBaseDto>(createdEntity);
        }

        public async Task UpdateAsync(int id, ContactInfoBaseDto dto)
        {
            var existingContactInfo = await _repository.GetByIdAsync(id);
            if (existingContactInfo == null)
            {
                throw new KeyNotFoundException($"ContactInfo with ID {id} not found.");
            }

            var updatedContactInfo = _mapper.Map(dto, existingContactInfo);
            await _repository.UpdateAsync(updatedContactInfo);
        }

        public async Task DeleteAsync(int id)
        {
            var existingContactInfo = await _repository.GetByIdAsync(id);
            if (existingContactInfo == null)
            {
                throw new KeyNotFoundException($"ContactInfo with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
