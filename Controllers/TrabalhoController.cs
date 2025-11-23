using Microsoft.AspNetCore.Mvc;
using StartTrekGS.src.StartTrekGS.Application.Service;
using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.Controllers
{
    [ApiController]
    [Route("trabalhos")]
    public class TrabalhoController : ControllerBase
    {
        private readonly TrabalhoService _trabalhoService;

        public TrabalhoController(TrabalhoService trabalhoService)
        {
            _trabalhoService = trabalhoService;
        }

        
        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            try
            {
                var lista = await _trabalhoService.ListarTodosAsync();

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
                    message = "Erro ao listar trabalhos."
                });
            }
        }

        
        [HttpGet("categoria/{idCategoria}")]
        public async Task<IActionResult> ListarPorCategoria(int idCategoria)
        {
            try
            {
                var lista = await _trabalhoService.ListarPorCategoriaAsync(idCategoria);
                return Ok(lista);
            }
            catch (Exception e)
            {
                return NotFound(new
                {
                    message = e.Message
                });
            }
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var trabalho = await _trabalhoService.BuscarPorIdAsync(id);
                return Ok(trabalho);
            }
            catch (Exception e)
            {
                return NotFound(new
                {
                    message = e.Message
                });
            }
        }

        
        [HttpPost("categoria/{idCategoria}")]
        public async Task<IActionResult> Cadastrar(int idCategoria, [FromBody] Trabalho trabalho)
        {
            try
            {
                var criado = await _trabalhoService.CadastrarAsync(idCategoria, trabalho);
                return CreatedAtAction(nameof(BuscarPorId), new { id = criado.IdTrabalho }, criado);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Trabalho trabalho)
        {
            try
            {
                var atualizado = await _trabalhoService.AtualizarAsync(id, trabalho);
                return Ok(atualizado);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _trabalhoService.ExcluirAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(new
                {
                    message = e.Message
                });
            }
        }
    }
}
