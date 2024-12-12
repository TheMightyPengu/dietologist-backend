using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

public interface IArticlesRepository
{
    Task<IEnumerable<Articles>> GetAllAsync();
    Task<Articles?> GetByIdAsync(int id);
    Task<Articles> AddAsync(Articles article);
    Task UpdateAsync(Articles article);
    Task DeleteAsync(int id);
}

public class ArticlesRepository(AppDbContext context) : IArticlesRepository
{
    public async Task<IEnumerable<Articles>> GetAllAsync()
    {
        return await context.Articles.ToListAsync();
    }

    public async Task<Articles?> GetByIdAsync(int id)
    {
        return await context.Articles.FindAsync(id);
    }

    public async Task<Articles> AddAsync(Articles article)
    {
        context.Articles.Add(article);
        await context.SaveChangesAsync();
        return article;
    }

    public async Task UpdateAsync(Articles article)
    {
        context.Entry(article).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var article = await context.Articles.FindAsync(id);
        if (article != null)
        {
            context.Articles.Remove(article);
            await context.SaveChangesAsync();
        }
    }
}