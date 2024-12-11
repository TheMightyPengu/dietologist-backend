using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dietologist_backend.Repository
{
    // Interface
    public interface IContactInfoRepository
    {
        Task<IEnumerable<ContactInfo>> GetAllAsync();
        Task<ContactInfo> GetByIdAsync(int id);
        Task<ContactInfo> AddAsync(ContactInfo contactInfo);
        Task UpdateAsync(ContactInfo contactInfo);
        Task DeleteAsync(int id);
    }

    // Implementation
    public class ContactInfoRepository : IContactInfoRepository
    {
        private readonly AppDbContext _context;

        public ContactInfoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactInfo>> GetAllAsync()
        {
            return await _context.ContactInfo.ToListAsync();
        }

        public async Task<ContactInfo> GetByIdAsync(int id)
        {
            return await _context.ContactInfo
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<ContactInfo> AddAsync(ContactInfo contactInfo)
        {
            _context.ContactInfo.Add(contactInfo);
            await _context.SaveChangesAsync();
            return contactInfo;
        }

        public async Task UpdateAsync(ContactInfo contactInfo)
        {
            var entity = await _context.ContactInfo.FindAsync(contactInfo.Id);

            if (entity == null)
            {
                throw new KeyNotFoundException($"ContactInfo with Id {contactInfo.Id} not found.");
            }

            entity.Email = contactInfo.Email;
            entity.Telephone = contactInfo.Telephone;
            entity.Location = contactInfo.Location;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ContactInfo.FindAsync(id);
            if (entity != null)
            {
                _context.ContactInfo.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"ContactInfo with Id {id} not found.");
            }
        }
    }
}
