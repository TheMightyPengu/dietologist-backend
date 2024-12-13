using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

// Interface
public interface IRecipesRepository
{
    Task<IEnumerable<Recipes>> GetAllAsync();
    Task<Recipes?> GetByIdAsync(int id);
    Task<Recipes> AddAsync(Recipes recipe);
    Task UpdateAsync(Recipes recipe);
    Task DeleteAsync(int id);
}

public class RecipesRepository : IRecipesRepository
{
    private readonly AppDbContext _context;

    public RecipesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Recipes>> GetAllAsync()
    {
        return await _context.Recipes.ToListAsync();
    }

    public async Task<Recipes?> GetByIdAsync(int id)
    {
        return await _context.Recipes.FindAsync(id);
    }

    public async Task<Recipes> AddAsync(Recipes recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task UpdateAsync(Recipes recipe)
    {
        _context.Entry(recipe).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }
    }
}
