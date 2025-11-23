using Microsoft.EntityFrameworkCore;
using StartTrekGS.Infrastructure;
using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public TipoUsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TipoUsuario>> GetAllAsync()
    {
        return await _context.TipoUsuarios.ToListAsync();
    }

    public async Task<TipoUsuario?> GetByIdAsync(int id)
    {
        return await _context.TipoUsuarios.FindAsync(id);
    }

    public async Task AddAsync(TipoUsuario tipoUsuario)
    {
        await _context.TipoUsuarios.AddAsync(tipoUsuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Update(tipoUsuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.TipoUsuarios.FindAsync(id);
        if (entity != null)
        {
            _context.TipoUsuarios.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
