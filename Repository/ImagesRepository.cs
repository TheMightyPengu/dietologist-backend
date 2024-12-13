using dietologist_backend.Data;
using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Repository;

// Interface
public interface IImagesRepository
{
    Task<IEnumerable<Images>> GetAllAsync();
    Task<Images?> GetByIdAsync(int id);
    Task<Images> AddAsync(Images image);
    Task UpdateAsync(Images image);
    Task DeleteAsync(int id);
}

public class ImagesRepository(AppDbContext context) : IImagesRepository
{
    public async Task<IEnumerable<Images>> GetAllAsync()
    {
        return await context.Images.ToListAsync();
    }

    public async Task<Images?> GetByIdAsync(int id)
    {
        return await context.Images.FindAsync(id);
    }

    public async Task<Images> AddAsync(Images image)
    {
        context.Images.Add(image);
        await context.SaveChangesAsync();
        return image;
    }

    public async Task UpdateAsync(Images image)
    {
        context.Entry(image).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var image = await context.Images.FindAsync(id);
        if (image != null)
        {
            context.Images.Remove(image);
            await context.SaveChangesAsync();
        }
    }
}
