using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

// Interface
public interface IResumeRepository
{
    Task<IEnumerable<Resume>> GetAllAsync();
    Task<Resume?> GetByIdAsync(int id);
    Task<Resume> AddAsync(Resume resume);
    Task UpdateAsync(Resume resume);
    Task DeleteAsync(int id);
}

public class ResumeRepository : IResumeRepository
{
    private readonly AppDbContext _context;

    public ResumeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Resume>> GetAllAsync()
    {
        return await _context.Resume.ToListAsync();
    }

    public async Task<Resume?> GetByIdAsync(int id)
    {
        return await _context.Resume.FindAsync(id);
    }


 

    public async Task<Resume> AddAsync(Resume resume)
    {
        _context.Resume.Add(resume);
        await _context.SaveChangesAsync();
        return resume;
    }
  
    public async Task UpdateAsync(Resume resume)
    {
        _context.Entry(resume).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var resume = await _context.Resume.FindAsync(id);
        if (resume != null)
        {
            _context.Resume.Remove(resume);
            await _context.SaveChangesAsync();
        }
    }
}
