using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

public interface ITipoUsuarioRepository
{
    Task<IEnumerable<TipoUsuario>> GetAllAsync();
    Task<TipoUsuario?> GetByIdAsync(int id);
    Task AddAsync(TipoUsuario tipoUsuario);
    Task UpdateAsync(TipoUsuario tipoUsuario);
    Task DeleteAsync(int id);
}
