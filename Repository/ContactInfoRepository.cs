using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

public interface IContactInfoRepository
{
    Task<IEnumerable<ContactInfo>> GetAllAsync();
    Task<ContactInfo?> GetByIdAsync(int id);
    Task<ContactInfo> AddAsync(ContactInfo contactInfo);
    Task UpdateAsync(ContactInfo contactInfo);
    Task DeleteAsync(int id);
}

public class ContactInfoRepository(AppDbContext context) : IContactInfoRepository
{
    public async Task<IEnumerable<ContactInfo>> GetAllAsync()
    {
        return await context.ContactInfo.ToListAsync();
    }

    public async Task<ContactInfo?> GetByIdAsync(int id)
    {
        return await context.ContactInfo.FindAsync(id);
    }

    public async Task<ContactInfo> AddAsync(ContactInfo contactInfo)
    {
        context.ContactInfo.Add(contactInfo);
        await context.SaveChangesAsync();
        return contactInfo;
    }

    public async Task UpdateAsync(ContactInfo contactInfo)
    {
        context.Entry(contactInfo).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.ContactInfo.FindAsync(id);
        if (entity != null)
        {
            context.ContactInfo.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}