using StartTrekGS.src.StartTrekGS.Application.DTO;
using StartTrekGS.src.StartTrekGS.Domain;
using StartTrekGS.src.StartTrekGS.Infrastructure.Repository;

namespace StartTrekGS.src.StartTrekGS.Application.Service
{
    public class ComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITrabalhoRepository _trabalhoRepository;

        public ComentarioService(
            IComentarioRepository comentarioRepository,
            IUsuarioRepository usuarioRepository,
            ITrabalhoRepository trabalhoRepository)
        {
            _comentarioRepository = comentarioRepository;
            _usuarioRepository = usuarioRepository;
            _trabalhoRepository = trabalhoRepository;
        }


        public async Task<IEnumerable<ComentarioResponseDto>> ListarTodosAsync()
        {
            var comentarios = await _comentarioRepository.GetAllAsync();

            return comentarios.Select(c => new ComentarioResponseDto
            {
                IdComentario = c.IdComentario,
                TextoComentario = c.TextoComentario,
                Ativo = c.Ativo,
                IdUsuario = c.IdUsuario,
                NomeUsuario = c.Usuario.NomeUsuario,
                IdTrabalho = c.IdTrabalho,
                NomeTrabalho = c.Trabalho.NomeTrabalho
            }).ToList();
        }

        public async Task<IEnumerable<ComentarioResponseDto>> ListarPorTrabalhoAsync(int idTrabalho)
        {
            var comentarios = await _comentarioRepository.GetAllAsync();

            return comentarios
                .Where(c => c.IdTrabalho == idTrabalho)
                .Select(c => new ComentarioResponseDto
                {
                    IdComentario = c.IdComentario,
                    TextoComentario = c.TextoComentario,
                    Ativo = c.Ativo,
                    IdUsuario = c.IdUsuario,
                    NomeUsuario = c.Usuario.NomeUsuario,
                    IdTrabalho = c.IdTrabalho,
                    NomeTrabalho = c.Trabalho.NomeTrabalho
                })
                .ToList();
        }

        public async Task<ComentarioResponseDto> CriarComentarioAsync(ComentarioCreateDto dto)
        {

            var usuario = await _usuarioRepository.GetByIdAsync(dto.IdUsuario)
                ?? throw new Exception("Usuário não encontrado");


            var trabalho = await _trabalhoRepository.GetByIdAsync(dto.IdTrabalho)
                ?? throw new Exception("Trabalho não encontrado");

            var comentario = new Comentario
            {
                TextoComentario = dto.TextoComentario,
                Ativo = true, 
                Usuario = usuario,
                IdUsuario = usuario.IdUsuario,
                Trabalho = trabalho,
                IdTrabalho = trabalho.IdTrabalho
            };

            await _comentarioRepository.AddAsync(comentario);

            return new ComentarioResponseDto
            {
                IdComentario = comentario.IdComentario,
                TextoComentario = comentario.TextoComentario,
                Ativo = comentario.Ativo,
                IdUsuario = usuario.IdUsuario,
                NomeUsuario = usuario.NomeUsuario,
                IdTrabalho = trabalho.IdTrabalho,
                NomeTrabalho = trabalho.NomeTrabalho
            };
        }
    }
}
