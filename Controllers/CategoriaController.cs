using Microsoft.AspNetCore.Mvc;
using StartTrekGS.src.StartTrekGS.Application.Service;
using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.Controllers
{
    [ApiController]
    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        
        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            try
            {
                var lista = await _categoriaService.ListarTodosAsync();

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
                    message = "Erro ao listar categorias."
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var categoria = await _categoriaService.BuscarPorIdAsync(id);
                return Ok(categoria);
            }
            catch (Exception e)
            {
                return NotFound(new
                {
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Categoria categoria)
        {
            try
            {
                var criado = await _categoriaService.CriarCategoriaAsync(categoria);
                return CreatedAtAction(nameof(BuscarPorId), new { id = criado.IdCategoria }, criado);
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
        public async Task<IActionResult> Atualizar(int id, [FromBody] Categoria categoria)
        {
            try
            {
                var atualizado = await _categoriaService.AtualizarCategoriaAsync(id, categoria);
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
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                await _categoriaService.ExcluirAsync(id);
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
