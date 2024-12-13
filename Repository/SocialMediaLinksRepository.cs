using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

// Interface
public interface ISocialMediaLinksRepository
{
    Task<IEnumerable<SocialMediaLinks>> GetAllAsync();
    Task<SocialMediaLinks?> GetByIdAsync(int id);
    Task<SocialMediaLinks> AddAsync(SocialMediaLinks link);
    Task UpdateAsync(SocialMediaLinks link);
    Task DeleteAsync(int id);
}

public class SocialMediaLinksRepository : ISocialMediaLinksRepository
{
    private readonly AppDbContext _context;

    public SocialMediaLinksRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SocialMediaLinks>> GetAllAsync()
    {
        return await _context.SocialMediaLinks.ToListAsync();
    }

    public async Task<SocialMediaLinks?> GetByIdAsync(int id)
    {
        return await _context.SocialMediaLinks.FindAsync(id);
    }

    public async Task<SocialMediaLinks> AddAsync(SocialMediaLinks link)
    {
        _context.SocialMediaLinks.Add(link);
        await _context.SaveChangesAsync();
        return link;
    }

    public async Task UpdateAsync(SocialMediaLinks link)
    {
        _context.Entry(link).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var link = await _context.SocialMediaLinks.FindAsync(id);
        if (link != null)
        {
            _context.SocialMediaLinks.Remove(link);
            await _context.SaveChangesAsync();
        }
    }
}
