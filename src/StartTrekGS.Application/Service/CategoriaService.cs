using StartTrekGS.src.StartTrekGS.Domain;
using StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

namespace StartTrekGS.src.StartTrekGS.Application.Service
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }


        public async Task<IEnumerable<Categoria>> ListarTodosAsync()
        {
            return await _categoriaRepository.GetAllAsync();
        }


        public async Task<Categoria> BuscarPorIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);

            if (categoria == null)
                throw new Exception($"Categoria não encontrada com ID: {id}");

            return categoria;
        }

        public async Task<Categoria> CriarCategoriaAsync(Categoria categoria)
        {
            ValidarCategoria(categoria);

            await _categoriaRepository.AddAsync(categoria);
            return categoria;
        }


        public async Task<Categoria> AtualizarCategoriaAsync(int id, Categoria novaCategoria)
        {
            var existente = await BuscarPorIdAsync(id);


            if (!string.IsNullOrWhiteSpace(novaCategoria.NomeCategoria))
                existente.NomeCategoria = novaCategoria.NomeCategoria;

            if (!string.IsNullOrWhiteSpace(novaCategoria.DescricaoCategoria))
                existente.DescricaoCategoria = novaCategoria.DescricaoCategoria;

            ValidarCategoria(existente);

            await _categoriaRepository.UpdateAsync(existente);
            return existente;
        }


        public async Task ExcluirAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);

            if (categoria == null)
                throw new Exception($"Categoria não encontrada para exclusão: {id}");

            await _categoriaRepository.DeleteAsync(id);
        }


        private void ValidarCategoria(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.NomeCategoria))
                throw new Exception("O nome da categoria é obrigatório.");

            if (string.IsNullOrWhiteSpace(categoria.DescricaoCategoria))
                throw new Exception("A descrição da categoria é obrigatória.");
        }
    }
}
