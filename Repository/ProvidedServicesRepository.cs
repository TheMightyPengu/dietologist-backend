using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository
{
    public interface IProvidedServicesRepository
    {
        Task<IEnumerable<ProvidedServices?>> GetAllAsync();
        Task<ProvidedServices?> GetByIdAsync(int id);
        Task<ProvidedServices?> AddAsync(ProvidedServices? providedService);
        Task UpdateAsync(ProvidedServices providedService);
        Task DeleteAsync(int id);
    }
    
    public class ProvidedServicesRepository(AppDbContext context) : IProvidedServicesRepository
    {
        public async Task<IEnumerable<ProvidedServices?>> GetAllAsync()
        {
            return await context.ProvidedServices.ToListAsync();
        }

        public async Task<ProvidedServices?> GetByIdAsync(int id)
        {
            return await context.ProvidedServices.FindAsync(id);
        }

        public async Task<ProvidedServices?> AddAsync(ProvidedServices? providedService)
        {
            context.ProvidedServices.Add(providedService);
            await context.SaveChangesAsync();
            return providedService;
        }

        public async Task UpdateAsync(ProvidedServices providedService)
        {
            context.Entry(providedService).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var providedService = await context.ProvidedServices.FindAsync(id);
            if (providedService != null)
            {
                context.ProvidedServices.Remove(providedService);
                await context.SaveChangesAsync();
            }
        }
    }
}