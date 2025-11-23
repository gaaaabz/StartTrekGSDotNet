using Microsoft.EntityFrameworkCore;
using StartTrekGS.Infrastructure;
using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

public class Esp32Repository : IEsp32Repository
{
    private readonly ApplicationDbContext _context;

    public Esp32Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Esp32>> GetAllAsync()
    {
        return await _context.Esp32.ToListAsync();
    }

    public async Task<Esp32?> GetByIdAsync(int id)
    {
        return await _context.Esp32.FindAsync(id);
    }

    public async Task AddAsync(Esp32 esp32)
    {
        await _context.Esp32.AddAsync(esp32);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Esp32 esp32)
    {
        _context.Esp32.Update(esp32);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Esp32.FindAsync(id);
        if (entity != null)
        {
            _context.Esp32.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
