using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

public interface IComentarioRepository
{
    Task<IEnumerable<Comentario>> GetAllAsync();
    Task<Comentario?> GetByIdAsync(int id);
    Task AddAsync(Comentario comentario);
    Task UpdateAsync(Comentario comentario);
    Task DeleteAsync(int id);
}
