using StartTrekGS.src.StartTrekGS.Domain;
using StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

namespace StartTrekGS.src.StartTrekGS.Application.Service
{
    public class TipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuarioService(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }

        public async Task<IEnumerable<TipoUsuario>> ListarTodosAsync()
        {
            return await _tipoUsuarioRepository.GetAllAsync();
        }


        public async Task<IEnumerable<TipoUsuario>> ListarTodosPaginadoAsync(int pagina, int tamanho)
        {
            var lista = await _tipoUsuarioRepository.GetAllAsync();

            return lista
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho)
                .ToList();
        }


        public async Task<TipoUsuario> BuscarPorIdAsync(int id)
        {
            var tipo = await _tipoUsuarioRepository.GetByIdAsync(id);

            if (tipo == null)
                throw new Exception($"Não encontramos o usuario com este ID: {id}");

            return tipo;
        }
    }
}
