using StartTrekGS.src.StartTrekGS.Application.DTO;
using StartTrekGS.src.StartTrekGS.Domain;
using StartTrekGS.src.StartTrekGS.Infrastructure.Repository;
using System.Linq;

namespace StartTrekGS.src.StartTrekGS.Application.Service
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;
        private readonly IEsp32Repository _esp32Repository;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            ITipoUsuarioRepository tipoUsuarioRepository,
            IEsp32Repository esp32Repository)
        {
            _usuarioRepository = usuarioRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
            _esp32Repository = esp32Repository;
        }



        public async Task<UsuarioResponseDto> CadastrarUsuarioAsync(UsuarioCreateDto dto)
        {
         
            var todos = await _usuarioRepository.GetAllAsync();
            if (todos.Any(u => u.Email == dto.Email))
                throw new Exception("Já existe um usuario cadastrado com este email.");

            var tipoUsuario = await _tipoUsuarioRepository.GetByIdAsync(dto.IdTipoUsuario!.Value);
            if (tipoUsuario == null)
                throw new Exception("Tipo de usuario inválido.");

            if (dto.Senha.Length < 8)
                throw new Exception("A senha deve ter pelo menos 8 caracteres.");

            var esp32Padrao = await _esp32Repository.GetByIdAsync(1);
            if (esp32Padrao == null)
                throw new Exception("ESP32 padrão (ID 1) não encontrado.");

            var novoUsuario = new Usuario
            {
                NomeUsuario = dto.NomeUsuario,
                Email = dto.Email,
                Senha = dto.Senha,
                TipoUsuario = tipoUsuario,
                Ativo = true,
                Esp32 = esp32Padrao,
                Foto = dto.Foto
            };

            await _usuarioRepository.AddAsync(novoUsuario);
            return ToResponseDto(novoUsuario);
        }

        public async Task<PagedResult<UsuarioResponseDto>> PesquisarAsync(
    string? nome,
    bool? ativo,
    int pagina,
    int tamanho,
    string ordenarPor,
    string direcao)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var query = usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(u =>
                    u.NomeUsuario.Contains(nome, StringComparison.OrdinalIgnoreCase));
            }

            if (ativo.HasValue)
            {
                query = query.Where(u => u.Ativo == ativo.Value);
            }

            bool desc = string.Equals(direcao, "desc", StringComparison.OrdinalIgnoreCase);

            query = ordenarPor?.ToLower() switch
            {
                "email" => desc
                    ? query.OrderByDescending(u => u.Email)
                    : query.OrderBy(u => u.Email),

                "id" => desc
                    ? query.OrderByDescending(u => u.IdUsuario)
                    : query.OrderBy(u => u.IdUsuario),

                _ => desc
                    ? query.OrderByDescending(u => u.NomeUsuario)
                    : query.OrderBy(u => u.NomeUsuario)
            };

            var total = query.Count();

            var dados = query
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho)
                .Select(ToResponseDto)
                .ToList();

            return new PagedResult<UsuarioResponseDto>
            {
                Pagina = pagina,
                Tamanho = tamanho,
                TotalRegistros = total,
                Dados = dados
            };
        }

        public async Task<IEnumerable<UsuarioResponseDto>> ListarUsuariosAsync()
        {
            var lista = await _usuarioRepository.GetAllAsync();
            return lista.Select(ToResponseDto).ToList();
        }

        public async Task<IEnumerable<UsuarioResponseDto>> ListarUsuariosPaginadoAsync(
            int pagina,
            int tamanho)
        {
            var lista = await _usuarioRepository.GetAllAsync();

            var paginaResultado = lista
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho)
                .Select(ToResponseDto)
                .ToList();

            return paginaResultado;
        }

        public async Task<UsuarioResponseDto> BuscarPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                throw new Exception($"Usuario não encontrado com o ID: {id}");

            return ToResponseDto(usuario);
        }

        public async Task<UsuarioResponseDto> AtualizarUsuarioAsync(int id, UsuarioUpdateDto dto)
        {
            var existente = await _usuarioRepository.GetByIdAsync(id);

            if (existente == null)
                throw new Exception($"Usuario não encontrado: {id}");

            if (!string.IsNullOrWhiteSpace(dto.NomeUsuario))
                existente.NomeUsuario = dto.NomeUsuario;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                existente.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Senha))
            {
                if (dto.Senha.Length < 8)
                    throw new Exception("A senha deve ter 8 caracteres no mínimo.");
                existente.Senha = dto.Senha;
            }

            if (dto.IdTipoUsuario != null)
            {
                var tipo = await _tipoUsuarioRepository.GetByIdAsync(dto.IdTipoUsuario.Value)
                    ?? throw new Exception("Tipo de usuário inválido.");

                existente.TipoUsuario = tipo;
            }

            if (dto.IdEsp32 != null)
            {
                var esp32 = await _esp32Repository.GetByIdAsync(dto.IdEsp32.Value)
                    ?? throw new Exception("ESP32 inválido.");

                existente.Esp32 = esp32;
            }

            if (dto.Ativo != null)
                existente.Ativo = dto.Ativo.Value;

            await _usuarioRepository.UpdateAsync(existente);
            return ToResponseDto(existente);
        }

        public async Task<UsuarioResponseDto> AtualizarFotoAsync(int id, UsuarioFotoDto dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id)
                ?? throw new Exception($"Usuario não encontrado com o ID: {id}");

            usuario.Foto = dto.Foto;

            await _usuarioRepository.UpdateAsync(usuario);
            return ToResponseDto(usuario);
        }

        public async Task DeletarUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                throw new Exception($"Usuario não encontrado com o ID: {id}");

            await _usuarioRepository.DeleteAsync(id);
        }

        private UsuarioResponseDto ToResponseDto(Usuario usuario)
        {
            return new UsuarioResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                NomeTipoUsuario = usuario.TipoUsuario?.NomeTipoUsuario,
                IdEsp32 = usuario.Esp32?.IdEsp32,
                Ativo = usuario.Ativo
            };
        }
    }
}