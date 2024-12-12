using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

public interface IEbooksRepository
{
    Task<IEnumerable<Ebooks>> GetAllAsync();
    Task<Ebooks?> GetByIdAsync(int id);
    Task<Ebooks> AddAsync(Ebooks ebook);
    Task UpdateAsync(Ebooks ebook);
    Task DeleteAsync(int id);
}

public class EbooksRepository(AppDbContext context) : IEbooksRepository
{
    public async Task<IEnumerable<Ebooks>> GetAllAsync()
    {
        return await context.Ebooks.ToListAsync();
    }

    public async Task<Ebooks?> GetByIdAsync(int id)
    {
        return await context.Ebooks.FindAsync(id);
    }

    public async Task<Ebooks> AddAsync(Ebooks ebook)
    {
        context.Ebooks.Add(ebook);
        await context.SaveChangesAsync();
        return ebook;
    }

    public async Task UpdateAsync(Ebooks ebook)
    {
        context.Entry(ebook).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.Ebooks.FindAsync(id);
        if (entity != null)
        {
            context.Ebooks.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}