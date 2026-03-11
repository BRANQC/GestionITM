using Microsoft.AspNetCore.Mvc;
using GestionITM.Domain.Interfaces;
using GestionITM.Domain.Dtos;

namespace GestionITM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        // El controlador depende del SERVICIO, nunca del repositorio directamente.
        // (El controlador es el mesero, no el almacenista)
        private readonly IProfesorService _service;

        public ProfesorController(IProfesorService service)
        {
            _service = service;
        }

        // GET: api/profesor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorDto>>> Get()
        {
            var profesoresDto = await _service.ObtenerTodosLosProfesoresAsync();
            return Ok(profesoresDto);
        }

        // POST: api/profesor
        [HttpPost]
        public async Task<ActionResult<ProfesorDto>> Post([FromBody] ProfesorCreateDto profesorCreateDto)
        {
            // El servicio contiene toda la lógica de negocio y validaciones.
            // Si algo falla, lanza una excepción que el ExceptionMiddleware captura.
            var resultado = await _service.RegistrarProfesorAsync(profesorCreateDto);
            return CreatedAtAction(nameof(Get), new { id = resultado.Id }, resultado);
        }
    }
}