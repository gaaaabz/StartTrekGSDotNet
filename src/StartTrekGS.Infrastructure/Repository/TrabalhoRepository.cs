using Microsoft.EntityFrameworkCore;
using StartTrekGS.Infrastructure;
using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

public class TrabalhoRepository : ITrabalhoRepository
{
    private readonly ApplicationDbContext _context;

    public TrabalhoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trabalho>> GetAllAsync()
    {
        return await _context.Trabalhos.ToListAsync();
    }

    public async Task<Trabalho?> GetByIdAsync(int id)
    {
        return await _context.Trabalhos.FindAsync(id);
    }

    public async Task AddAsync(Trabalho trabalho)
    {
        await _context.Trabalhos.AddAsync(trabalho);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Trabalho trabalho)
    {
        _context.Trabalhos.Update(trabalho);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Trabalhos.FindAsync(id);
        if (entity != null)
        {
            _context.Trabalhos.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
