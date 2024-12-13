using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

// Interface
public interface INewsletterSubscribersRepository
{
    Task<IEnumerable<NewsletterSubscribers>> GetAllAsync();
    Task<NewsletterSubscribers?> GetByIdAsync(int id);
    Task<NewsletterSubscribers> AddAsync(NewsletterSubscribers subscriber);
    Task UpdateAsync(NewsletterSubscribers subscriber);
    Task DeleteAsync(int id);
}

public class NewsletterSubscribersRepository(AppDbContext context) : INewsletterSubscribersRepository
{
    public async Task<IEnumerable<NewsletterSubscribers>> GetAllAsync()
    {
        return await context.NewsletterSubscribers.ToListAsync();
    }

    public async Task<NewsletterSubscribers?> GetByIdAsync(int id)
    {
        return await context.NewsletterSubscribers.FindAsync(id);
    }

    public async Task<NewsletterSubscribers> AddAsync(NewsletterSubscribers subscriber)
    {
        context.NewsletterSubscribers.Add(subscriber);
        await context.SaveChangesAsync();
        return subscriber;
    }

    public async Task UpdateAsync(NewsletterSubscribers subscriber)
    {
        context.Entry(subscriber).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var subscriber = await context.NewsletterSubscribers.FindAsync(id);
        if (subscriber != null)
        {
            context.NewsletterSubscribers.Remove(subscriber);
            await context.SaveChangesAsync();
        }
    }
}
