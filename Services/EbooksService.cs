using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dietologist_backend.Services
{
    public interface IEbooksService
    {
        Task<IEnumerable<EbooksBaseDto>> GetAllDtosAsync();
        Task<EbooksBaseDto> GetDtoByIdAsync(int id);
        Task<EbooksBaseDto> AddAsync(EbooksBaseDto dto);
        Task UpdateAsync(int id, EbooksBaseDto dto);
        Task DeleteAsync(int id);
    }

    public class EbooksService : IEbooksService
    {
        private readonly IEbooksRepository _repository;
        private readonly IMapper _mapper;

        public EbooksService(IEbooksRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EbooksBaseDto>> GetAllDtosAsync()
        {
            var ebooks = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<EbooksBaseDto>>(ebooks);
        }

        public async Task<EbooksBaseDto> GetDtoByIdAsync(int id)
        {
            var ebook = await _repository.GetByIdAsync(id);
            if (ebook == null)
            {
                throw new KeyNotFoundException($"Ebook with ID {id} not found.");
            }
            return _mapper.Map<EbooksBaseDto>(ebook);
        }

        public async Task<EbooksBaseDto> AddAsync(EbooksBaseDto dto)
        {
            var entity = _mapper.Map<Ebooks>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<EbooksBaseDto>(createdEntity);
        }

        public async Task UpdateAsync(int id, EbooksBaseDto dto)
        {
            var existingEbook = await _repository.GetByIdAsync(id);
            if (existingEbook == null)
            {
                throw new KeyNotFoundException($"Ebook with ID {id} not found.");
            }

            var updatedEbook = _mapper.Map(dto, existingEbook);
            await _repository.UpdateAsync(updatedEbook);
        }

        public async Task DeleteAsync(int id)
        {
            var existingEbook = await _repository.GetByIdAsync(id);
            if (existingEbook == null)
            {
                throw new KeyNotFoundException($"Ebook with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
