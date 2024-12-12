using AutoMapper;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using dietologist_backend.Repository;
using FluentValidation;

namespace dietologist_backend.Services
{
    public interface IArticlesService
    {
        Task<IEnumerable<ArticlesBaseDto>> GetAllDtosAsync();
        Task<ArticlesBaseDto?> GetDtoByIdAsync(int id);
        Task<ArticlesBaseDto> AddAsync(ArticlesBaseDto dto);
        Task UpdateAsync(int id, ArticlesBaseDto dto);
        Task DeleteAsync(int id);
    }

    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<ArticlesBaseDto> _validator;

        public ArticlesService(
            IArticlesRepository repository, 
            IMapper mapper, 
            IValidator<ArticlesBaseDto> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<IEnumerable<ArticlesBaseDto>> GetAllDtosAsync()
        {
            var articles = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ArticlesBaseDto>>(articles);
        }

        public async Task<ArticlesBaseDto?> GetDtoByIdAsync(int id)
        {
            var article = await FindArticleByIdAsync(id);
            return _mapper.Map<ArticlesBaseDto>(article);
        }

        public async Task<ArticlesBaseDto> AddAsync(ArticlesBaseDto dto)
        {
            await ValidateDtoAsync(dto);

            var entity = _mapper.Map<Articles>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<ArticlesBaseDto>(createdEntity);
        }

        public async Task UpdateAsync(int id, ArticlesBaseDto dto)
        {
            await ValidateDtoAsync(dto);

            var existingArticle = await FindArticleByIdAsync(id);
            _mapper.Map(dto, existingArticle);
            await _repository.UpdateAsync(existingArticle);
        }

        public async Task DeleteAsync(int id)
        {
            var existingArticle = await FindArticleByIdAsync(id);
            await _repository.DeleteAsync(existingArticle.Id);
        }

        private async Task<Articles> FindArticleByIdAsync(int id)
        {
            var article = await _repository.GetByIdAsync(id);
            if (article == null)
                throw new KeyNotFoundException($"Article with ID {id} not found.");
            
            return article;
        }

        private async Task ValidateDtoAsync(ArticlesBaseDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException("Validation failed", validationResult.Errors);
        }
    }
}