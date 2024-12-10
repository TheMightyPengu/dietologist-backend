using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository
{
    // Interface
    public interface IAppointmentsRepository
    {
        Task<IEnumerable<Appointments>> GetAllAsync();
        Task<Appointments> GetByIdAsync(int id);
        Task<Appointments> AddAsync(Appointments appointment);
        Task UpdateAsync(Appointments appointment);
        Task DeleteAsync(int id);
    }

    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly AppDbContext _context;

        public AppointmentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointments>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointments> GetByIdAsync(int id)
        {
            return await _context.Appointments
               .Include(a => a.ProvidedService)
               .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Appointments> AddAsync(Appointments appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task UpdateAsync(Appointments appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
