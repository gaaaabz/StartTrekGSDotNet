using Microsoft.AspNetCore.Mvc;
using StartTrekGS.src.StartTrekGS.Application.DTO;
using StartTrekGS.src.StartTrekGS.Application.Service;

namespace StartTrekGS.Controllers
{
    [ApiController]
    [Route("comentarios")]
    public class ComentarioController : ControllerBase
    {
        private readonly ComentarioService _comentarioService;

        public ComentarioController(ComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            try
            {
                var lista = await _comentarioService.ListarTodosAsync();

                if (!lista.Any())
                    return NoContent();

                int inicio = (pagina - 1) * tamanho;
                int fim = Math.Min(inicio + tamanho, lista.Count());

                var paginaResultado = lista
                    .Skip(inicio)
                    .Take(tamanho)
                    .ToList();

                var resposta = new
                {
                    pagina,
                    tamanho,
                    totalRegistros = lista.Count(),
                    dados = paginaResultado
                };

                return Ok(resposta);
            }
            catch
            {
                return StatusCode(500, new
                {
                    message = "Erro ao listar comentários."
                });
            }
        }

        [HttpGet("trabalho/{idTrabalho}")]
        public async Task<IActionResult> ListarPorTrabalho(int idTrabalho)
        {
            try
            {
                var lista = await _comentarioService.ListarPorTrabalhoAsync(idTrabalho);
                return Ok(lista);
            }
            catch (Exception e)
            {
                return StatusCode(404, new
                {
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ComentarioCreateDto dto)
        {
            try
            {
                

                var criado = await _comentarioService.CriarComentarioAsync(dto);
                return StatusCode(201, criado);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }
    }
}
