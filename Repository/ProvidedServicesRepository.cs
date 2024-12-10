using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository
{
    // Interface
    public interface IProvidedServicesRepository
    {
        Task<IEnumerable<ProvidedServices>> GetAllAsync();
        Task<ProvidedServices> GetByIdAsync(int id);
        Task<ProvidedServices> AddAsync(ProvidedServices providedService);
        Task UpdateAsync(ProvidedServices providedService);
        Task DeleteAsync(int id);
    }
    
    public class ProvidedServicesRepository : IProvidedServicesRepository
    {
        private readonly AppDbContext _context;

        public ProvidedServicesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProvidedServices>> GetAllAsync()
        {
            return await _context.ProvidedServices.ToListAsync();
        }

        public async Task<ProvidedServices> GetByIdAsync(int id)
        {
            return (await _context.ProvidedServices.FindAsync(id));
        }

        public async Task<ProvidedServices> AddAsync(ProvidedServices providedService)
        {
            _context.ProvidedServices.Add(providedService);
            await _context.SaveChangesAsync();
            return providedService;
        }

        public async Task UpdateAsync(ProvidedServices providedService)
        {
            _context.Entry(providedService).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _context.ProvidedServices.FindAsync(id);
            if (service != null)
            {
                _context.ProvidedServices.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}