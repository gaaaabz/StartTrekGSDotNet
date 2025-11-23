using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

public interface IEsp32Repository
{
    Task<IEnumerable<Esp32>> GetAllAsync();
    Task<Esp32?> GetByIdAsync(int id);
    Task AddAsync(Esp32 esp32);
    Task UpdateAsync(Esp32 esp32);
    Task DeleteAsync(int id);
}
