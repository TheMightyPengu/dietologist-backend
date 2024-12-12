using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

public interface IAppointmentsRepository
{
    Task<IEnumerable<Appointments>> GetAllAsync();
    Task<Appointments?> GetByIdAsync(int id);
    Task<Appointments> AddAsync(Appointments appointment);
    Task UpdateAsync(Appointments appointment);
    Task DeleteAsync(int id);
}

public class AppointmentsRepository(AppDbContext context) : IAppointmentsRepository
{
    public async Task<IEnumerable<Appointments>> GetAllAsync()
    {
        return await context.Appointments.ToListAsync();
    }

    public async Task<Appointments?> GetByIdAsync(int id)
    {
        return await context.Appointments
            .Include(a => a.ProvidedService)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Appointments> AddAsync(Appointments appointment)
    {
        context.Appointments.Add(appointment);
        await context.SaveChangesAsync();
        return appointment;
    }

    public async Task UpdateAsync(Appointments appointment)
    {
        context.Entry(appointment).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            context.Appointments.Remove(appointment);
            await context.SaveChangesAsync();
        }
    }
}