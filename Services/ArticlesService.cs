using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dietologist_backend.Services
{
    public interface IArticlesService
    {
        Task<IEnumerable<ArticlesBaseDto>> GetAllDtosAsync();
        Task<ArticlesBaseDto> GetDtoByIdAsync(int id);
        Task<ArticlesBaseDto> AddAsync(ArticlesBaseDto dto);
        Task UpdateAsync(int id, ArticlesBaseDto dto);
        Task DeleteAsync(int id);
    }

    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesRepository _repository;
        private readonly IMapper _mapper;

        public ArticlesService(IArticlesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticlesBaseDto>> GetAllDtosAsync()
        {
            var articles = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ArticlesBaseDto>>(articles);
        }

        public async Task<ArticlesBaseDto> GetDtoByIdAsync(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            if (article == null)
            {
                throw new KeyNotFoundException($"Article with ID {id} not found.");
            }
            return _mapper.Map<ArticlesBaseDto>(article);
        }

        public async Task<ArticlesBaseDto> AddAsync(ArticlesBaseDto dto)
        {
            var entity = _mapper.Map<Articles>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<ArticlesBaseDto>(createdEntity);
        }

        public async Task UpdateAsync(int id, ArticlesBaseDto dto)
        {
            var existingArticle = await _repository.GetByIdAsync(id);
            if (existingArticle == null)
            {
                throw new KeyNotFoundException($"Article with ID {id} not found.");
            }

            var updatedArticle = _mapper.Map(dto, existingArticle);
            await _repository.UpdateAsync(updatedArticle);
        }

        public async Task DeleteAsync(int id)
        {
            var existingArticle = await _repository.GetByIdAsync(id);
            if (existingArticle == null)
            {
                throw new KeyNotFoundException($"Article with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
