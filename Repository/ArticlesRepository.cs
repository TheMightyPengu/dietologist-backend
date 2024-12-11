using dietologist_backend.Data;
using dietologist_backend.DTO;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dietologist_backend.Repository
{
    // Interface
    public interface IArticlesRepository
    {
        Task<IEnumerable<Articles>> GetAllAsync();
        Task<Articles> GetByIdAsync(int id);
        Task<Articles> AddAsync(Articles article);
        Task UpdateAsync(Articles article);
        Task DeleteAsync(int id);
    }

    public class ArticlesRepository : IArticlesRepository
    {
        private readonly AppDbContext _context;

        public ArticlesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Articles>> GetAllAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<Articles> GetByIdAsync(int id)
        {
            return await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Articles> AddAsync(Articles article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task UpdateAsync(Articles article)
        {
            _context.Entry(article).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }
    }
}
