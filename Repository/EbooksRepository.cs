using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dietologist_backend.Repository
{
    public interface IEbooksRepository
    {
        Task<IEnumerable<Ebooks>> GetAllAsync();
        Task<Ebooks> GetByIdAsync(int id);
        Task<Ebooks> AddAsync(Ebooks ebook);
        Task UpdateAsync(Ebooks ebook);
        Task DeleteAsync(int id);
    }

    public class EbooksRepository : IEbooksRepository
    {
        private readonly AppDbContext _context;

        public EbooksRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ebooks>> GetAllAsync()
        {
            return await _context.Ebooks.ToListAsync();
        }

        public async Task<Ebooks> GetByIdAsync(int id)
        {
            return await _context.Ebooks.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Ebooks> AddAsync(Ebooks ebook)
        {
            _context.Ebooks.Add(ebook);
            await _context.SaveChangesAsync();
            return ebook;
        }

        public async Task UpdateAsync(Ebooks ebook)
        {
            _context.Entry(ebook).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Ebooks.FindAsync(id);
            if (entity != null)
            {
                _context.Ebooks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
