using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dietologist_backend.Services
{
    public interface IContactMessagesService
    {
        Task<IEnumerable<ContactMessagesBaseDto>> GetAllDtosAsync();
        Task<ContactMessagesBaseDto> GetDtoByIdAsync(int id);
        Task<ContactMessagesBaseDto> AddAsync(ContactMessagesBaseDto dto);
        Task UpdateAsync(int id, ContactMessagesBaseDto dto);
        Task DeleteAsync(int id);
    }

    public class ContactMessagesService : IContactMessagesService
    {
        private readonly IContactMessagesRepository _repository;
        private readonly IMapper _mapper;

        public ContactMessagesService(IContactMessagesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactMessagesBaseDto>> GetAllDtosAsync()
        {
            var messages = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactMessagesBaseDto>>(messages);
        }

        public async Task<ContactMessagesBaseDto> GetDtoByIdAsync(int id)
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null)
            {
                throw new KeyNotFoundException($"ContactMessage with ID {id} not found.");
            }
            return _mapper.Map<ContactMessagesBaseDto>(message);
        }

        public async Task<ContactMessagesBaseDto> AddAsync(ContactMessagesBaseDto dto)
        {
            var entity = _mapper.Map<ContactMessages>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<ContactMessagesBaseDto>(createdEntity);
        }

        public async Task UpdateAsync(int id, ContactMessagesBaseDto dto)
        {
            var existingMessage = await _repository.GetByIdAsync(id);
            if (existingMessage == null)
            {
                throw new KeyNotFoundException($"ContactMessage with ID {id} not found.");
            }

            var updatedMessage = _mapper.Map(dto, existingMessage);
            await _repository.UpdateAsync(updatedMessage);
        }

        public async Task DeleteAsync(int id)
        {
            var existingMessage = await _repository.GetByIdAsync(id);
            if (existingMessage == null)
            {
                throw new KeyNotFoundException($"ContactMessage with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
