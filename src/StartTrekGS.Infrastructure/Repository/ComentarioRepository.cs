using Microsoft.EntityFrameworkCore;
using StartTrekGS.Infrastructure;
using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

public class ComentarioRepository : IComentarioRepository
{
    private readonly ApplicationDbContext _context;

    public ComentarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Comentario>> GetAllAsync()
    {
        return await _context.Comentarios.ToListAsync();
    }

    public async Task<Comentario?> GetByIdAsync(int id)
    {
        return await _context.Comentarios.FindAsync(id);
    }

    public async Task AddAsync(Comentario comentario)
    {
        await _context.Comentarios.AddAsync(comentario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Comentario comentario)
    {
        _context.Comentarios.Update(comentario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Comentarios.FindAsync(id);
        if (entity != null)
        {
            _context.Comentarios.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
