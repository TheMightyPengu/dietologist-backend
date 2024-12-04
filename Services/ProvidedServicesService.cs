using dietologist_backend.Models;
using dietologist_backend.Repository;

namespace dietologist_backend.Services
{
    
    public interface IProvidedServicesService
    {
        Task<IEnumerable<ProvidedServices?>> GetAllAsync();
        Task<ProvidedServices?> GetByIdAsync(int id);
        Task<ProvidedServices?> AddAsync(ProvidedServices? providedService);
        Task UpdateAsync(ProvidedServices providedService);
        Task DeleteAsync(int id);
    }
    
    public class ProvidedServicesService(IProvidedServicesRepository repository) : IProvidedServicesService
    {
        public async Task<IEnumerable<ProvidedServices?>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<ProvidedServices?> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<ProvidedServices?> AddAsync(ProvidedServices? providedService)
        {
            var response = await repository.AddAsync(providedService);
            if (response == null)
            {
                throw new Exception("Saving the ProvidedService failed.");
            }
            
            return response;
        }

        public async Task UpdateAsync(ProvidedServices providedService)
        {
            await repository.UpdateAsync(providedService);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(id);
        }
    }
}