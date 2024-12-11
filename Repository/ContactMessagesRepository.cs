using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dietologist_backend.Repository
{
    public interface IContactMessagesRepository
    {
        Task<IEnumerable<ContactMessages>> GetAllAsync();
        Task<ContactMessages> GetByIdAsync(int id);
        Task<ContactMessages> AddAsync(ContactMessages contactMessage);
        Task UpdateAsync(ContactMessages contactMessage);
        Task DeleteAsync(int id);
    }

    public class ContactMessagesRepository : IContactMessagesRepository
    {
        private readonly AppDbContext _context;

        public ContactMessagesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactMessages>> GetAllAsync()
        {
            return await _context.ContactMessages.ToListAsync();
        }

        public async Task<ContactMessages> GetByIdAsync(int id)
        {
            return await _context.ContactMessages.FirstOrDefaultAsync(cm => cm.Id == id);
        }

        public async Task<ContactMessages> AddAsync(ContactMessages contactMessage)
        {
            _context.ContactMessages.Add(contactMessage);
            await _context.SaveChangesAsync();
            return contactMessage;
        }

        public async Task UpdateAsync(ContactMessages contactMessage)
        {
            _context.Entry(contactMessage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ContactMessages.FindAsync(id);
            if (entity != null)
            {
                _context.ContactMessages.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
