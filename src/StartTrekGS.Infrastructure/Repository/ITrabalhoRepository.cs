using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

public interface ITrabalhoRepository
{
    Task<IEnumerable<Trabalho>> GetAllAsync();
    Task<Trabalho?> GetByIdAsync(int id);
    Task AddAsync(Trabalho trabalho);
    Task UpdateAsync(Trabalho trabalho);
    Task DeleteAsync(int id);
}
