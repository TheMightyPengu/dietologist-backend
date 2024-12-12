using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

public interface IContactMessagesRepository
{
    Task<IEnumerable<ContactMessages>> GetAllAsync();
    Task<ContactMessages?> GetByIdAsync(int id);
    Task<ContactMessages> AddAsync(ContactMessages contactMessage);
    Task UpdateAsync(ContactMessages contactMessage);
    Task DeleteAsync(int id);
}

public class ContactMessagesRepository(AppDbContext context) : IContactMessagesRepository
{
    public async Task<IEnumerable<ContactMessages>> GetAllAsync()
    {
        return await context.ContactMessages.ToListAsync();
    }

    public async Task<ContactMessages?> GetByIdAsync(int id)
    {
        return await context.ContactMessages.FindAsync(id);
    }

    public async Task<ContactMessages> AddAsync(ContactMessages contactMessage)
    {
        context.ContactMessages.Add(contactMessage);
        await context.SaveChangesAsync();
        return contactMessage;
    }

    public async Task UpdateAsync(ContactMessages contactMessage)
    {
        context.Entry(contactMessage).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.ContactMessages.FindAsync(id);
        if (entity != null)
        {
            context.ContactMessages.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}