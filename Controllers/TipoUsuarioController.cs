using Microsoft.AspNetCore.Mvc;
using StartTrekGS.src.StartTrekGS.Application.Service;
using StartTrekGS.src.StartTrekGS.Domain;

namespace StartTrekGS.Controllers
{
    [ApiController]
    [Route("tipo-usuario")]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly TipoUsuarioService _tipoUsuarioService;

        public TipoUsuarioController(TipoUsuarioService tipoUsuarioService)
        {
            _tipoUsuarioService = tipoUsuarioService;
        }

        
        [HttpGet]
        public async Task<IActionResult> ListarTodosPaginado(
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanho = 10)
        {
            var lista = await _tipoUsuarioService.ListarTodosPaginadoAsync(pagina, tamanho);

            if (!lista.Any())
                return NoContent();

            return Ok(lista);
        }

        
        [HttpGet("todos-tipos")]
        public async Task<IActionResult> ListarTodos()
        {
            var todos = await _tipoUsuarioService.ListarTodosAsync();

            if (!todos.Any())
                return NoContent();

            return Ok(todos);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var tipoUsuario = await _tipoUsuarioService.BuscarPorIdAsync(id);
                return Ok(tipoUsuario);
            }
            catch (Exception)
            {
                return NotFound($"Tipo de usuário não encontrado com ID: {id}");
            }
        }
    }
}
