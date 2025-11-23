using StartTrekGS.src.StartTrekGS.Domain;
using StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

namespace StartTrekGS.src.StartTrekGS.Application.Service
{
    public class TrabalhoService
    {
        private readonly ITrabalhoRepository _trabalhoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public TrabalhoService(
            ITrabalhoRepository trabalhoRepository,
            ICategoriaRepository categoriaRepository)
        {
            _trabalhoRepository = trabalhoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Trabalho>> ListarTodosAsync()
        {
            return await _trabalhoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Trabalho>> ListarPorCategoriaAsync(int idCategoria)
        {
            var lista = await _trabalhoRepository.GetAllAsync();
            return lista.Where(t => t.IdCategoria == idCategoria).ToList();
        }

        public async Task<Trabalho> BuscarPorIdAsync(int id)
        {
            var trabalho = await _trabalhoRepository.GetByIdAsync(id);

            if (trabalho == null)
                throw new Exception($"Trabalho não encontrado com este ID: {id}");

            return trabalho;
        }

        public async Task<Trabalho> CadastrarAsync(int idCategoria, Trabalho trabalho)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(idCategoria)
                ?? throw new Exception($"Categoria não encontrada: {idCategoria}");

            ValidarNomeELimite(trabalho.NomeTrabalho);
            await ValidarDuplicidadeDeTitulo(trabalho.NomeTrabalho, null);

            trabalho.Categoria = categoria;
            trabalho.IdCategoria = categoria.IdCategoria;

            await _trabalhoRepository.AddAsync(trabalho);
            return trabalho;
        }

        public async Task<Trabalho> AtualizarAsync(int id, Trabalho novoTrabalho)
        {
            var existente = await BuscarPorIdAsync(id);

            if (!string.IsNullOrWhiteSpace(novoTrabalho.NomeTrabalho))
            {
                ValidarNomeELimite(novoTrabalho.NomeTrabalho);
                await ValidarDuplicidadeDeTitulo(novoTrabalho.NomeTrabalho, id);
                existente.NomeTrabalho = novoTrabalho.NomeTrabalho;
            }

            if (!string.IsNullOrWhiteSpace(novoTrabalho.DescricaoTrabalho))
            {
                existente.DescricaoTrabalho = novoTrabalho.DescricaoTrabalho;
            }

            if (novoTrabalho.Categoria != null)
            {
                var categoria = await _categoriaRepository.GetByIdAsync(
                    novoTrabalho.Categoria.IdCategoria
                ) ?? throw new Exception("Categoria inválida.");

                existente.Categoria = categoria;
                existente.IdCategoria = categoria.IdCategoria;
            }

            await _trabalhoRepository.UpdateAsync(existente);
            return existente;
        }

        public async Task ExcluirAsync(int id)
        {
            var existente = await _trabalhoRepository.GetByIdAsync(id);

            if (existente == null)
                throw new Exception($"Id deste trabalho não existe: {id}");

            await _trabalhoRepository.DeleteAsync(id);
        }


        private void ValidarNomeELimite(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome para este campo é obrigatório");

            if (nome.Length > 150)
                throw new Exception("Limite de 150 caracteres excedido");
        }

        private async Task ValidarDuplicidadeDeTitulo(string titulo, int? idAtual)
        {
            var lista = await _trabalhoRepository.GetAllAsync();

            bool duplicado = lista.Any(t =>
                t.NomeTrabalho != null &&
                t.NomeTrabalho.Equals(titulo, StringComparison.OrdinalIgnoreCase) &&
                (idAtual == null || t.IdTrabalho != idAtual)
            );

            if (duplicado)
                throw new Exception("Este título já existe");
        }
    }
}
